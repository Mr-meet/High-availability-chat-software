package com.dao;

import com.common.GenericHibernateDAO;
import com.entity.DictionaryEntity;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.stereotype.Repository;

/**
 * Created by mrfj on 2017/5/6.
 */

@Repository("dictionaryDAO")
public class DictionaryDAO extends GenericHibernateDAO<DictionaryEntity,String> {
    public static Log log = LogFactory.getLog(DictionaryDAO.class);
    public DictionaryDAO() {
    }
}