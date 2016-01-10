var RapCypher = RapCypher || {};

RapCypher.ViewModel = (function() {
    var viewModel = {
        Users: ko.mapping.fromJS([]), // List of users that are logged in and ready for connections
        Cypher1Users: ko.mapping.fromJS([]),
        Cypher2Users: ko.mapping.fromJS([]),
        Cypher3Users: ko.mapping.fromJS([]),
        Cypher4Users: ko.mapping.fromJS([]),
        Cypher5Users: ko.mapping.fromJS([]),
        Cypher1Text: ko.observable('Join'),
        Cypher2Text: ko.observable('Join'),
        Cypher3Text: ko.observable('Join'),
        Cypher4Text: ko.observable('Join'),
        Cypher5Text: ko.observable('Join'),
        Cypher1Css: ko.observable('btn btn-success'),
        Cypher2Css: ko.observable('btn btn-success'),
        Cypher3Css: ko.observable('btn btn-success'),
        Cypher4Css: ko.observable('btn btn-success'),
        Cypher5Css: ko.observable('btn btn-success'),
        Username: ko.observable('not logged in.'), // My username, to be reflected in UI
        MyConnectionId: ko.observable(''), // My connection Id, so I can tell who I am
        Mode: ko.observable('idle'), // UI mode ['idle', 'calling', 'incall']
        Loading: ko.observable(false) // Loading indicator control
    };

    // The user that represents me
    viewModel.Me = ko.computed(function() {
        return ko.utils.arrayFirst(this.Users(), function(user) {
            return user.ConnectionId() == viewModel.MyConnectionId();
        });
    }, viewModel);

    viewModel.MeCypher1 = ko.computed(function() {
        return ko.utils.arrayFirst(this.Cypher1Users(), function(user) {
            return user.ConnectionId() == viewModel.MyConnectionId();
        });
    }, viewModel);

    viewModel.MeCypher2 = ko.computed(function() {
        return ko.utils.arrayFirst(this.Cypher2Users(), function(user) {
            return user.ConnectionId() == viewModel.MyConnectionId();
        });
    }, viewModel);

    viewModel.MeCypher3 = ko.computed(function() {
        return ko.utils.arrayFirst(this.Cypher3Users(), function(user) {
            return user.ConnectionId() == viewModel.MyConnectionId();
        });
    }, viewModel);

    viewModel.MeCypher4 = ko.computed(function() {
        return ko.utils.arrayFirst(this.Cypher4Users(), function(user) {
            return user.ConnectionId() == viewModel.MyConnectionId();
        });
    }, viewModel);

    viewModel.MeCypher5 = ko.computed(function() {
        return ko.utils.arrayFirst(this.Cypher5Users(), function(user) {
            return user.ConnectionId() == viewModel.MyConnectionId();
        });
    }, viewModel);

    // The readable status of the UI
    viewModel.CallStatus = ko.computed(function() {
        var callStatus;
        if (this.Mode() == 'idle') {
            callStatus = 'Idle';
        } else if (this.Mode() == 'calling') {
            callStatus = 'Calling...';
        } else {
            callStatus = 'In Call';
        }
        return callStatus;
    }, viewModel);

    // Set a new array of users.  We could simply do viewModel.Users([array]),
    // but the mapping plugin converts all the user props to observables for us.
    viewModel.setUsers = function(userArray) {
        ko.mapping.fromJS(userArray, viewModel.Users);
    };

    viewModel.setCypherUsers = function(cypherUserArray, cypherId) {
        switch (cypherId) {
        case 1:
            ko.mapping.fromJS(cypherUserArray, viewModel.Cypher1Users);
            break;
        case 2:
            ko.mapping.fromJS(cypherUserArray, viewModel.Cypher2Users);
            break;
        case 3:
            ko.mapping.fromJS(cypherUserArray, viewModel.Cypher3Users);
            break;
        case 4:
            ko.mapping.fromJS(cypherUserArray, viewModel.Cypher4Users);
            break;
        case 5:
            ko.mapping.fromJS(cypherUserArray, viewModel.Cypher5Users);
            break;
        default:
            break;
        }
    };

    viewModel.updateCypherState = function(cypherId) {
        switch (cypherId) {
        case 1:
            if (viewModel.MeCypher1()) {
                if (viewModel.MeCypher1().InCypher()) {
                    viewModel.Cypher1Text('Leave');
                    viewModel.Cypher1Css('btn btn-danger');
                } else {
                    viewModel.Cypher1Text('Join');
                    viewModel.Cypher1Css('btn btn-success');
                }
            } else {
                viewModel.Cypher1Text('Join');
                viewModel.Cypher1Css('btn btn-success');
            }
            break;
        case 2:
            if (viewModel.MeCypher2()) {
                if (viewModel.MeCypher2().InCypher()) {
                    viewModel.Cypher2Text('Leave');
                    viewModel.Cypher2Css('btn btn-danger');
                } else {
                    viewModel.Cypher2Text('Join');
                    viewModel.Cypher2Css('btn btn-success');
                }
            } else {
                viewModel.Cypher2Text('Join');
                viewModel.Cypher2Css('btn btn-success');
            }
            break;
        case 3:
            if (viewModel.MeCypher3()) {
                if (viewModel.MeCypher3().InCypher()) {
                    viewModel.Cypher3Text('Leave');
                    viewModel.Cypher3Css('btn btn-danger');
                } else {
                    viewModel.Cypher3Text('Join');
                    viewModel.Cypher3Css('btn btn-success');
                }
            } else {
                viewModel.Cypher3Text('Join');
                viewModel.Cypher3Css('btn btn-success');
            }
            break;
        case 4:
            if (viewModel.MeCypher4()) {
                if (viewModel.MeCypher4().InCypher()) {
                    viewModel.Cypher4Text('Leave');
                    viewModel.Cypher4Css('btn btn-danger');
                } else {
                    viewModel.Cypher4Text('Join');
                    viewModel.Cypher4Css('btn btn-success');
                }
            } else {
                viewModel.Cypher4Text('Join');
                viewModel.Cypher4Css('btn btn-success');
            }
            break;
        case 5:
            if (viewModel.MeCypher5()) {
                if (viewModel.MeCypher5().InCypher()) {
                    viewModel.Cypher5Text('Leave');
                    viewModel.Cypher5Css('btn btn-danger');
                } else {
                    viewModel.Cypher5Text('Join');
                    viewModel.Cypher5Css('btn btn-success');
                }
            } else {
                viewModel.Cypher5Text('Join');
                viewModel.Cypher5Css('btn btn-success');
            }
            break;
        default:
            break;
        }
    };

    // Retreives the css class that should be used to represent the user status.
    // I can't get this to work as just a dynamic class property for some reason.
    viewModel.getUserStatus = function(user) {
        var css;
        if (user == viewModel.Me()) {
            css = 'icon-user';
        } else if (user.InCypher()) {
            css = 'icon-phone-3';
        } else {
            css = 'icon-phone-4';
        }
        return css;
    };
    ko.applyBindings(viewModel);
    return viewModel;
})();