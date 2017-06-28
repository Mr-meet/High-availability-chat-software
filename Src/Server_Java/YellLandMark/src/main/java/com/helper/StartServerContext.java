package com.helper;

import com.entity.CheckMessage;
import com.entity.DictionaryEntity;
import com.service.DictionaryService;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

/**
 * Created by mrfj on 2017/5/6.
 */
public class StartServerContext {
    public static Log log = LogFactory.getLog(StartServerContext.class);
    public static ApplicationContext CTX = null;
    public static ApplicationContext RBBIT=null;
    private static DictionaryService dictionaryService=null;
    public static ExecutorService pool = null;
    private static final int POOL_THEAD_NUMBER=10;

    public StartServerContext() {
    }

    public static boolean initSpring()
    {
        try
        {
            if(CTX == null)
                CTX = new ClassPathXmlApplicationContext("applicationContext.xml");
            if (RBBIT==null)
                RBBIT=new ClassPathXmlApplicationContext("spring-rabbitmq-producer.xml");
        }
        catch(Exception e)
        {
            log.error((new StringBuilder("Init Spring ApplicationContext ERROR:")).append(e.getMessage()).toString());
            e.printStackTrace();
            return false;
        }
        return true;
    }

    public static void createPool()
    {
        pool = Executors.newFixedThreadPool(POOL_THEAD_NUMBER);
    }

    public static void initDictionary(){
        dictionaryService=DictionaryService.getFromApplication();
        List<String> stringList=new ArrayList<>();
        //查找敏感词汇
        List<DictionaryEntity> dictionaryEntityList=dictionaryService.findDictionaryByUseage("sensitive_vocabulary");
        for (DictionaryEntity dictionaryEntity:dictionaryEntityList) {
            stringList.add(dictionaryEntity.getValue());
        }
        CheckMessage.stringListHashMap.put("sensitive_vocabulary",stringList);

        stringList=new ArrayList<>();
        //查找诈骗嫌疑词汇
         dictionaryEntityList=dictionaryService.findDictionaryByUseage("fraud_suspects");
        for (DictionaryEntity dictionaryEntity:dictionaryEntityList) {
            stringList.add(dictionaryEntity.getValue());
        }
        CheckMessage.stringListHashMap.put("fraud_suspects",stringList);


        //查找诈骗嫌疑词汇
        dictionaryEntityList=dictionaryService.findDictionaryByUseage("ServerMessage");
        for (DictionaryEntity dictionaryEntity:dictionaryEntityList) {
            CheckMessage.stringStringHashMap.put(dictionaryEntity.getName(),dictionaryEntity.getValue());
        }


    }
}
