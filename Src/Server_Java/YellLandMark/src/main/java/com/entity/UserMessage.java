package com.entity;

import java.util.Arrays;
import java.util.Date;

/**
 * Created by mrfj on 2017/5/6.
 */
public class UserMessage {

    //消息ID
    private String id=null;
    //發送者的
    private String senderId=null;
    //接收者的ID
    private String resiverId=null;
    //消息發送的時間
    private String sendTime=null;
    //用戶所使用的平台
    private Integer platform=null;
    private Boolean  contentChecked;
    private Integer resiverPlatform;
    private String content;
    private byte[] photoContent;
    private String attachedMessage;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getSenderId() {
        return senderId;
    }

    public void setSenderId(String senderId) {
        this.senderId = senderId;
    }

    public String getResiverId() {
        return resiverId;
    }

    public void setResiverId(String resiverId) {
        this.resiverId = resiverId;
    }

    public String getSendTime() {
        return sendTime;
    }

    public void setSendTime(String sendTime) {
        this.sendTime = sendTime;
    }

    public Integer getPlatform() {
        return platform;
    }

    public void setPlatform(Integer platform) {
        this.platform = platform;
    }

    public Boolean getContentChecked() {
        return contentChecked;
    }

    public void setContentChecked(Boolean contentChecked) {
        this.contentChecked = contentChecked;
    }

    public Integer getResiverPlatform() {
        return resiverPlatform;
    }

    public void setResiverPlatform(Integer resiverPlatform) {
        this.resiverPlatform = resiverPlatform;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public byte[] getPhotoContent() {
        return photoContent;
    }

    public void setPhotoContent(byte[] photoContent) {
        this.photoContent = photoContent;
    }

    public String getAttachedMessage() {
        return attachedMessage;
    }

    public void setAttachedMessage(String attachedMessage) {
        this.attachedMessage = attachedMessage;
    }

    @Override
    public String toString() {
        return "UserMessage{" +
                "id='" + id + '\'' +
                ", senderId='" + senderId + '\'' +
                ", resiverId='" + resiverId + '\'' +
                ", sendTime=" + sendTime +
                ", platform=" + platform +
                ", contentChecked=" + contentChecked +
                ", resiverPlatform=" + resiverPlatform +
                ", content='" + content + '\'' +
                ", photoContent=" + Arrays.toString(photoContent) +
                ", attachedMessage='" + attachedMessage + '\'' +
                '}';
    }
}
