package Core;
/*Rap Battle Online: Class PrivateMessage*/

public class PrivateMessage {
    private String details;
    private int messageId;
    private int userId;
    private String to;
    private String sentBy;
    private String subject;
    private int dateSent;
    private Boolean isDeleted;
    private Boolean isArchived;
    private Boolean isReply;
    private Boolean isInOutbox;
    private Boolean isRead;

    public String getDetails() {
        return details;
    }

    public void setDetails(String details) {
        this.details = details;
    }

    public int getMessageId() {
        return messageId;
    }

    public void setMessageId(int messageId) {
        this.messageId = messageId;
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public String getTo() {
        return to;
    }

    public void setTo(String to) {
        this.to = to;
    }

    public String getSentBy() {
        return sentBy;
    }

    public void setSentBy(String sentBy) {
        this.sentBy = sentBy;
    }

    public String getSubject() {
        return subject;
    }

    public void setSubject(String subject) {
        this.subject = subject;
    }

    public int getDateSent() {
        return dateSent;
    }

    public void setDateSent(int dateSent) {
        this.dateSent = dateSent;
    }

    public Boolean getIsDeleted() {
        return isDeleted;
    }

    public void setIsDeleted(Boolean isDeleted) {
        this.isDeleted = isDeleted;
    }

    public Boolean getIsArchived() {
        return isArchived;
    }

    public void setIsArchived(Boolean isArchived) {
        this.isArchived = isArchived;
    }

    public Boolean getIsReply() {
        return isReply;
    }

    public void setIsReply(Boolean isReply) {
        this.isReply = isReply;
    }

    public Boolean getIsInOutbox() {
        return isInOutbox;
    }

    public void setIsInOutbox(Boolean isInOutbox) {
        this.isInOutbox = isInOutbox;
    }

    public Boolean getIsRead() {
        return isRead;
    }

    public void setIsRead(Boolean isRead){
        this.isRead = isRead;
    }

}
