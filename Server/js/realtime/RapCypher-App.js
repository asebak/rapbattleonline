var RapCypher = RapCypher || {};

RapCypher.App = (function(viewModel, connectionManager) {
    var _mediaStream,
        _hub,

        //connect to SignalR        
        _connect = function(username, onSuccess, onFailure) {
            var hub = $.connection.rapCypher;
            $.support.cors = true;
            $.connection.hub.url = '/signalr/hubs';
            $.connection.hub.start()
                .done(function() {
                    hub.server.connect(username);
                    if (onSuccess) {
                        onSuccess(hub);
                    }
                })
                .fail(function(event) {
                    if (onFailure) {
                        onFailure(event);
                    }
                });
            // Setup client SignalR operations
            _setupHubCallbacks(hub);
            _hub = hub;
        },

        _start = function(pageUserName) {
            // Show warning if WebRTC support is not detected
            if (webrtcDetectedBrowser == null) {
                $('.instructions').hide();
                $('.browser-warning').show();
            }
            _startSession(pageUserName);
        },

        _startSession = function(username) {
            viewModel.Username(username); // Set the selected username in the UI
            viewModel.Loading(true); // Turn on the loading indicator

            // Ask the user for permissions to access the webcam and mic
            getUserMedia(
                {
                    video: false,
                    audio: true
                },
                function(stream) { // succcess callback gives us a media stream
                    $('.instructions').hide();

                    // Now we have everything we need for interaction, so fire up SignalR
                    _connect(username, function(hub) {
                        // tell the viewmodel our conn id, so we can be treated like the special person we are.
                        viewModel.MyConnectionId(hub.connection.id);

                        // Initialize our client signal manager, giving it a signaler (the SignalR hub) and some callbacks
                        console.log('initializing connection manager');
                        connectionManager.initialize(hub.server, _callbacks.onReadyForStream, _callbacks.onStreamAdded, _callbacks.onStreamRemoved);

                        // Store off the stream reference so we can share it later
                        _mediaStream = stream;

                        // Load the stream into a video element so it starts playing in the UI
                        console.log('playing my local video feed');
                        var videoElement = document.querySelector('.video.mine');
                        attachMediaStream(videoElement, _mediaStream);

                        // Hook up the UI
                        _attachUiHandlers();

                        viewModel.Loading(false);
                    }, function(event) {
                        alertify.alert('<h4>Failed SignalR Connection</h4> We were not able to connect you to the signaling server.<br/><br/>Error: ' + JSON.stringify(event));
                        viewModel.Loading(false);
                    });
                },
                function(error) { // error callback
                    alertify.alert('<h4>Failed to get hardware access!</h4> Do you have another browser type open and using your cam/mic?<br/><br/>You were not connected to the server, because I didn\'t code to make browsers without media access work well. <br/><br/>Actual Error: ' + JSON.stringify(error));
                    viewModel.Loading(false);
                }
            );
        },

        _attachUiHandlers = function() {
            // Add click handler to users in the "Users" pane
            $('.user').live('click', function() {
                // Find the target user's SignalR client id
                var targetConnectionId = $(this).attr('data-cid');

                // Make sure we are in a state where we can make a call
                if (viewModel.Mode() !== 'idle') {
                    alertify.error('Sorry, you are already in a call.  Conferencing is not yet implemented.');
                    return;
                }

                // Then make sure we aren't calling ourselves.
                if (targetConnectionId != viewModel.MyConnectionId()) {
                    // Initiate a call
                    _hub.server.callUser(targetConnectionId);

                    // UI in calling mode
                    viewModel.Mode('calling');
                } else {
                    alertify.error("Ah, nope.  Can't call yourself.");
                }
            });

            // Add handler for the hangup button
            $('.hangup').click(function() {
                // Only allow hangup if we are not idle
                if (viewModel.Mode() != 'idle') {
                    _hub.server.hangUp();
                    connectionManager.closeAllConnections();
                    viewModel.Mode('idle');
                }
            });

            $('.cypher1').click(function() {
                if (viewModel.MeCypher1()) {
                    if (!viewModel.MeCypher1().InCypher()) {
                        _hub.server.joinCypher(1);
                    } else {
                        _hub.server.leaveCypher(1);
                    }
                } else {
                    _hub.server.joinCypher(1);
                }
            });

            $('.cypher2').click(function() {
                if (viewModel.MeCypher2()) {
                    if (!viewModel.MeCypher2().InCypher()) {
                        _hub.server.joinCypher(2);
                    } else {
                        _hub.server.leaveCypher(2);
                    }
                } else {
                    _hub.server.joinCypher(2);
                }
            });

            $('.cypher3').click(function() {
                if (viewModel.MeCypher3()) {
                    if (!viewModel.MeCypher3().InCypher()) {
                        _hub.server.joinCypher(3);
                    } else {
                        _hub.server.leaveCypher(3);
                    }
                } else {
                    _hub.server.joinCypher(3);
                }
            });

            $('.cypher4').click(function() {
                if (viewModel.MeCypher4()) {
                    if (!viewModel.MeCypher4().InCypher()) {
                        _hub.server.joinCypher(4);
                    } else {
                        _hub.server.leaveCypher(4);
                    }
                } else {
                    _hub.server.joinCypher(4);
                }
            });

            $('.cypher5').click(function() {
                if (viewModel.MeCypher5()) {
                    if (!viewModel.MeCypher5().InCypher()) {
                        _hub.server.joinCypher(5);
                    } else {
                        _hub.server.leaveCypher(5);
                    }
                } else {
                    _hub.server.joinCypher(5);
                }
            });
        },

        _setupHubCallbacks = function(hub) {
            // Hub Callback: Incoming Call
            hub.client.incomingCall = function(callingUser) {
                console.log('incoming call from: ' + JSON.stringify(callingUser));

                // Ask if we want to talk
                alertify.confirm(callingUser.UserName + ' is calling.  Do you want to chat?', function(e) {
                    if (e) {
                        // I want to chat
                        hub.server.answerCall(true, callingUser.ConnectionId);

                        // So lets go into call mode on the UI
                        viewModel.Mode('incall');
                    } else {
                        // Go away, I don't want to chat with you
                        hub.server.answerCall(false, callingUser.ConnectionId);
                    }
                });
            };

            // Hub Callback: Call Accepted
            hub.client.callAccepted = function(acceptingUser) {
                console.log('call accepted from: ' + JSON.stringify(acceptingUser) + '.  Initiating WebRTC call and offering my stream up...');

                // Callee accepted our call, let's send them an offer with our video stream
                connectionManager.initiateOffer(acceptingUser.ConnectionId, _mediaStream);

                // Set UI into call mode
                viewModel.Mode('incall');
            };

            // Hub Callback: Call Declined
            hub.client.callDeclined = function(decliningConnectionId, reason) {
                console.log('call declined from: ' + decliningConnectionId);

                // Let the user know that the callee declined to talk
                alertify.error(reason);

                // Back to an idle UI
                viewModel.Mode('idle');
            };

            // Hub Callback: Call Ended
            hub.client.callEnded = function(connectionId, reason) {
                console.log('call with ' + connectionId + ' has ended: ' + reason);

                // Let the user know why the server says the call is over
                alertify.error(reason);

                // Close the WebRTC connection
                connectionManager.closeConnection(connectionId);

                // Set the UI back into idle mode
                viewModel.Mode('idle');
            };

            // Hub Callback: Update User List
            hub.client.updateUserList = function(userList) {
                viewModel.setUsers(userList);
            };

            //Update a specific cypherlist
            hub.client.updateCypherList = function(cypherList, cypherId) {
                viewModel.setCypherUsers(cypherList, cypherId);
                viewModel.updateCypherState(cypherId);
            };

            hub.client.sendNotification = function(message, type) {
                switch (type) {
                case 0:
                    alertify.error(message);
                    break;
                case 1:
                    alertify.success(message);
                    break;
                default:
                    alertify.log(message);
                    break;
                }
            };

            // Hub Callback: WebRTC Signal Received
            hub.client.receiveSignal = function(callingUser, data) {
                connectionManager.newSignal(callingUser.ConnectionId, data);
            };
        },

        // Connection Manager Callbacks
        _callbacks = {
            onReadyForStream: function(connection) {
                // The connection manager needs our stream
                // todo: not sure I like this
                connection.addStream(_mediaStream);
            },
            onStreamAdded: function(connection, event) {
                console.log('binding remote stream to the partner window');

                // Bind the remote stream to the partner window
                var otherVideo = document.querySelector('.video.partner');
                attachMediaStream(otherVideo, event.stream); // from adapter.js
            },
            onStreamRemoved: function(connection, streamId) {
                // todo: proper stream removal.  right now we are only set up for one-on-one which is why this works.
                console.log('removing remote stream from partner window');

                // Clear out the partner window
                var otherVideo = document.querySelector('.video.partner');
                otherVideo.src = '';
            }
        };

    return {
        start: _start, // Starts the UI process
        getStream: function() { // Temp hack for the connection manager to reach back in here for a stream
            return _mediaStream;
        }
    };
})(RapCypher.ViewModel, RapCypher.ConnectionManager);