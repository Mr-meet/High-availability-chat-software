import com.entity.UserMessage;
import com.helper.StartServerContext;
import com.processer.RabbitMessageSender;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import java.util.UUID;

/**
 * Created by mrfj on 2017/5/5.
 */
public class StartServer {
    private static final Log log = LogFactory.getLog(StartServer.class);


    public void start() throws InterruptedException {
        if (StartServerContext.initSpring()){
            StartServerContext.createPool();
            StartServerContext.initDictionary();
            RabbitMessageSender rabbitMessageSender=RabbitMessageSender.getFromApplication();
            /*for (int i=0;i<1;i++){
                UserMessage userMessage =createUserMessage();
                rabbitMessageSender.sendToSendQueue(userMessage);
                Thread.sleep(10000);
                userMessage.setContent("答辩完我们好好休息一下吧？");
                rabbitMessageSender.sendToSendQueue(userMessage);
            }*/

            log.error("OK!");
        }
    }

    private UserMessage createUserMessage(){
        UserMessage userMessage =new UserMessage();
        userMessage.setId(UUID.randomUUID().toString());
        userMessage.setPlatform(0);
        userMessage.setSenderId(UUID.randomUUID().toString());
        userMessage.setResiverId(UUID.randomUUID().toString());
        userMessage.setContent("馮俊超級超級帥！完全不想自杀，嗯！就是醬紫！");
        userMessage.setContentChecked(false);
        return userMessage;
    }

    public static void main(String[] args) {
        StartServer startServer=new StartServer();
        try {
            startServer.start();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}
