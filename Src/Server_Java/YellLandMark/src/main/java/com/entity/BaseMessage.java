package com.entity;

import java.util.Date;

/**
 * Created by mrfj on 2017/5/6.
 */
public interface BaseMessage {
    //消息ID
     String id=null;
     //發送者的
     String senderId=null;
     //接收者的ID
     String resiverId=null;
     //消息發送的時間
     Date sendTime=null;
     //用戶所使用的平台
     Integer platform=null;

     public String getId();

     public void setId(String id);

     public String getSenderId();
     public void  setSenderId(String senderId);
     public String getResiverId();
     public void  setResiverId(String resiverId);
     public Integer getPlatform();
     public void  setPlatform(Integer platform);
     public Date getSendTime();
     public void setSendTime(Date sendTime) ;

     public String toString();
}
