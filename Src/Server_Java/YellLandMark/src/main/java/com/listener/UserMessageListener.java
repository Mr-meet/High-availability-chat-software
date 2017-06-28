package com.listener;

import com.entity.UserMessage;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.helper.StartServerContext;
import com.processer.MessageHandler;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.amqp.core.Message;
import org.springframework.amqp.core.MessageListener;
import org.springframework.stereotype.Component;

import java.io.IOException;

/**
 * Created by mrfj on 2017/5/5.
 */
@Component("userMessageListener")
public class UserMessageListener implements MessageListener {

    private static final Log log = LogFactory.getLog(UserMessageListener.class);
    private UserMessage userMessage;

    @Override
    public void onMessage(Message message) {
       // log.error("Resave a message:"+message.toString());
        String recvStr = new String(message.getBody());
        ObjectMapper mapper = new ObjectMapper();
        try {
            userMessage =mapper.readValue(recvStr,UserMessage.class);
            Thread thread=new MessageHandler(userMessage);
            if (thread!=null){
                StartServerContext.pool.execute(thread);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
