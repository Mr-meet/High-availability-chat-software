package com.processer;

import com.helper.StartServerContext;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.amqp.core.AmqpTemplate;
import org.springframework.stereotype.Service;
import com.config.*;
/**
 * Created by mrfj on 2017/5/6.
 */
@Service("rabbitMessageSender")
public class RabbitMessageSender {
    private static final Log log = LogFactory.getLog(RabbitMessageSender.class);
    // @Autowired
    protected AmqpTemplate amqpTemplate;
    private String exchange = RabbitExchange.QUEUE_DIRECT;
    private String communityProcessQueue= RabbitQueue.COMMUNITY_PROCESS_QUEUE;
    private String userSendQueue=RabbitQueue.USER_SEND_QUEUE;
    private String userResaveQueue=RabbitQueue.USER_RESAVE_QUEUE;

    public RabbitMessageSender() {
    }

    public <T> void sendToResaveQueue( final  Object value) {
        amqpTemplate=(AmqpTemplate)StartServerContext.RBBIT.getBean("amqpTemplate");
        amqpTemplate.convertAndSend( exchange,userResaveQueue, value);
        log.warn("发送了一次回执消息:"+value.toString());
    }

    public <T> void sendToSendQueue( final  Object value) {
        amqpTemplate=(AmqpTemplate)StartServerContext.RBBIT.getBean("amqpTemplate");
        amqpTemplate.convertAndSend( exchange,userSendQueue, value);
        log.info("发送了一次回执消息:"+value.toString());
    }

    public <T> void sendToCommunityProcessQueue( final  Object value) {
        amqpTemplate=(AmqpTemplate)StartServerContext.RBBIT.getBean("amqpTemplate");
        amqpTemplate.convertAndSend( exchange,communityProcessQueue, value);
        log.info("发送了一次回执消息:"+value.toString());
    }

    public static RabbitMessageSender getFromApplication() {
        return (RabbitMessageSender) StartServerContext.CTX.getBean("rabbitMessageSender");
    }
}
