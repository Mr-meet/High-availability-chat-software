package com.common;

import org.hibernate.transform.ResultTransformer;

import java.util.List;

public class ResultTransformerImpl implements ResultTransformer {
    private static final long serialVersionUID = 1L;

    public ResultTransformerImpl() {
    }

    public List transformList(List arg0) {
        return arg0;
    }

    public Object transformTuple(Object[] tuple, String[] aliases) {
        DBRecord record = new DBRecord();

        for(int i = 0; i < aliases.length; ++i) {
            if(tuple[i] instanceof Character) {
                tuple[i] = String.valueOf(tuple[i]);
            }

            record.addField(aliases[i], tuple[i]);
        }

        return record;
    }
}