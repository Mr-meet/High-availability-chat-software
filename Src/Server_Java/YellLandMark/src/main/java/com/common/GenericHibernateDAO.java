package com.common;

/**
 * Created by mrfj on 2017/5/6.
 */

import org.hibernate.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.orm.hibernate4.HibernateCallback;
import org.springframework.orm.hibernate4.support.HibernateDaoSupport;

import javax.persistence.Table;
import java.io.Serializable;
import java.lang.reflect.Field;
import java.lang.reflect.ParameterizedType;
import java.util.*;

public class GenericHibernateDAO<T, PK extends Serializable> extends HibernateDaoSupport {
    private Class<T> persistentClass;

    public GenericHibernateDAO() {
        this.persistentClass = (Class)
                ((ParameterizedType) this.getClass().getGenericSuperclass())
                        .getActualTypeArguments()[0];
    }

    @Autowired
    public void setSessionFactoryOverride(SessionFactory sessionFactory) {

        super.setSessionFactory(sessionFactory);
    }

    public GenericHibernateDAO(Class<T> persistentClass) {

        this.persistentClass = persistentClass;
    }

    public Session getSession() {
        Session session = this.getSessionFactory().openSession();
        session.setFlushMode(FlushMode.AUTO);

        return session;
    }


    public void releaseSession(Session session) {
        session.close();
    }


    public java.io.Serializable save(T entity) throws Exception {

        return super.getHibernateTemplate().save(entity);
    }

    public void saveAll(final List<T> entities) throws Exception {
        super.getHibernateTemplate().executeWithNativeSession(new HibernateCallback() {
            public Object doInHibernate(Session session) throws HibernateException {
                ListIterator ite = entities.listIterator();

                while (ite.hasNext()) {
                    Object entity = ite.next();
                    session.save(entity);
                    if (ite.nextIndex() % 30 == 0) {
                        session.flush();
                        session.clear();
                    }
                }

                session.flush();
                session.clear();
                return null;
            }
        });
    }

    public void update(T entity) throws Exception {
        super.getHibernateTemplate().update(entity);
    }

    public Object merge(T entity) {
        return this.getHibernateTemplate().merge(entity);
    }

    public void updateAll(final List<T> entities) throws Exception {
        super.getHibernateTemplate().executeWithNativeSession(new HibernateCallback() {
            public Object doInHibernate(Session session) throws HibernateException {
                ListIterator ite = entities.listIterator();

                while (ite.hasNext()) {
                    Object entity = ite.next();
                    session.update(entity);
                    if (ite.nextIndex() % 30 == 0) {
                        session.flush();
                        session.clear();
                    }
                }

                session.flush();
                session.clear();
                return null;
            }
        });
    }

    public void saveOrUpdate(T entity) throws Exception {
        super.getHibernateTemplate().saveOrUpdate(entity);
    }

    public void saveOrUpdateAll(final List<T> entities) throws Exception {
        super.getHibernateTemplate().executeWithNativeSession(new HibernateCallback() {
            public Object doInHibernate(Session session)
                    throws HibernateException {
                ListIterator ite = entities.listIterator();

                while (ite.hasNext()) {
                    Object entity = ite.next();
                    session.saveOrUpdate(entity);
                    if (ite.nextIndex() % 30 == 0) {
                        session.flush();
                        session.clear();
                    }
                }

                session.flush();
                session.clear();
                return null;
            }
        });
    }

    public void delete(T entity) throws Exception {
        super.getHibernateTemplate().delete(entity);
    }

    public void deleteById(PK id) throws Exception {
        super.getHibernateTemplate().delete(this.findById(id));
    }

    public void deleteALl(final List<T> entities) throws Exception {
        super.getHibernateTemplate().executeWithNativeSession(new HibernateCallback() {
            public Object doInHibernate(Session session) throws HibernateException {
                ListIterator ite = entities.listIterator();

                while (ite.hasNext()) {
                    Object entity = ite.next();
                    session.delete(entity);
                    if (ite.nextIndex() % 30 == 0) {
                        session.flush();
                        session.clear();
                    }
                }

                session.flush();
                session.clear();
                return null;
            }
        });
    }

    public T findById(PK id) {
        T entity = super.getHibernateTemplate().get(this.persistentClass, id);
        return entity;
    }

    public List findByProperty(String propertyName, Object value) {
        String queryString = "from " + this.persistentClass.getName() + " as model where model." + propertyName + "=?";
        return super.getHibernateTemplate().find(queryString, value);
    }

    public List<T> findByExample(T entity) {

        return super.getHibernateTemplate().findByExample(entity);
    }

    public List<T> findAll() {
        new ArrayList();
        Object list = super.getHibernateTemplate().loadAll(this.persistentClass);
        if (list == null) {
            list = new ArrayList();
        }

        return (List) list;
    }

    public List<T> findByNamedQuery(String queryName, Map<String, Object> queryParams) {
        new ArrayList();
        String[] params = new String[queryParams.size()];
        Object[] values = new Object[queryParams.size()];
        int index = 0;

        String key;
        for (Iterator iteKey = queryParams.keySet().iterator();
             iteKey.hasNext();
             values[index++] = queryParams.get(key)) {
            key = (String) iteKey.next();
            params[index] = key;
        }

        Object list = super.getHibernateTemplate().findByNamedQueryAndNamedParam(queryName,
                params, values);
        if (list == null) {
            list = new ArrayList();
        }

        return (List) list;
    }

    public Integer execBySql(final String sql, final List<T> params) {
        this.getHibernateTemplate().executeWithNativeSession(new HibernateCallback() {
            public Object doInHibernate(Session session) throws HibernateException {
                SQLQuery query = session.createSQLQuery(sql);
                if (params != null) {
                    for (int i = 0; i < params.size(); ++i) {
                        query.setParameter(i, params.get(i));
                    }
                }
                return query.executeUpdate();
            }
        });
        return 0;
    }

    public List<T> findBySql(final String sql, final List<T> params) {
        return (List) this.getHibernateTemplate().executeWithNativeSession(new HibernateCallback() {
            public Object doInHibernate(Session session) throws HibernateException {
                SQLQuery query = session.createSQLQuery(sql).addEntity(
                        GenericHibernateDAO.this.persistentClass);
                if (params != null) {
                    for (int i = 0; i < params.size(); ++i) {
                        query.setParameter(i, params.get(i));
                    }
                }

                return query.list();
            }
        });
    }

    public List<DBRecord> findResultBySql(final String sql, final List<T> params) {
        return (List) this.getHibernateTemplate().executeWithNativeSession(new HibernateCallback() {
            public Object doInHibernate(Session session) throws HibernateException {
                SQLQuery query = session.createSQLQuery(sql);
                if (params != null) {
                    for (int i = 0; i < params.size(); ++i) {
                        query.setParameter(i, params.get(i));
                    }
                }
                return query.setResultTransformer(new ResultTransformerImpl()).list();
            }
        });
    }

    public boolean exists(PK id) {
        Object entity = super.getHibernateTemplate().get(this.persistentClass, id);
        return entity != null;
    }


}
