package com.processer;

import com.entity.CheckMessage;
import com.entity.UserMessage;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

/**
 * Created by mrfj on 2017/5/6.
 */
public class MessageHandler extends Thread {

    public static Log log = LogFactory.getLog(MessageHandler.class);
    private UserMessage userMessage;
    public MessageHandler() {
    }

    public MessageHandler(UserMessage userMessage){
        this.userMessage = userMessage;

    }


    @Override
    public void run() {
        String userMessageContent= userMessage.getContent();
        //检查诈骗
        for (String stringVocabulary: CheckMessage.stringListHashMap.get("fraud_suspects")) {
            if (userMessageContent.contains(stringVocabulary)){
                userMessage.setContentChecked(true);
                userMessage.setAttachedMessage(CheckMessage.stringStringHashMap.get("scam"));
                log.warn("Find a fraud_suspects ["+stringVocabulary+"] message! userID:"+userMessage.getSenderId());

            }
        }

        //检查敏感言论
        for (String stringVocabulary: CheckMessage.stringListHashMap.get("sensitive_vocabulary")) {
            if (userMessageContent.contains(stringVocabulary)){
                userMessage.setContentChecked(true);
                userMessage.setAttachedMessage(CheckMessage.stringStringHashMap.get("reactionary"));
                userMessage.setContent(userMessageContent.replace(stringVocabulary,"!@#$%"));
                log.warn("Find a fraud_suspects ["+stringVocabulary+"] message! userID:"+userMessage.getSenderId());

            }
        }

        RabbitMessageSender.getFromApplication().sendToResaveQueue(userMessage);
        return;

    }

    @Override
    protected void finalize() throws Throwable {
        super.finalize();
    }
}
