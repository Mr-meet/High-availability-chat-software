package com.service;

import com.dao.DictionaryDAO;
import com.entity.DictionaryEntity;
import com.helper.StartServerContext;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * Created by mrfj on 2017/5/6.
 */
@Service("dictionaryService")
public class DictionaryService {
    @Autowired
    private DictionaryDAO dictionaryDAO;
    public static Log log = LogFactory.getLog(DictionaryService.class);

    public static DictionaryService getFromApplication() {
        return (DictionaryService) StartServerContext.CTX.getBean("dictionaryService");
    }
    public DictionaryService() {
    }

    public List<DictionaryEntity> findDictionaryByUseage(String useage)
    {
        List<DictionaryEntity> dictionaryEntityList=null;
        try {
            dictionaryEntityList=dictionaryDAO.findByProperty("useage",useage);
        }catch (Exception e){
            e.printStackTrace();
        }
        return dictionaryEntityList;
    }


}
