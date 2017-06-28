package com.common;

import java.math.BigDecimal;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.util.HashMap;
import java.util.Map;

public class DBRecord {
    private Map<String, Object> valueMap = new HashMap();

    public DBRecord() {
    }

    public DBRecord(ResultSet rs) {
        try {
            ResultSetMetaData e = rs.getMetaData();
            int culumnCount = e.getColumnCount();

            for(int i = 1; i <= culumnCount; ++i) {
                String columnName = e.getColumnName(i).toLowerCase();
                int columnType = e.getColumnType(i);
                Object columnValue = rs.getObject(i);


                this.addField(columnName, columnValue);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    public void addField(String colmunname, Object value) {
        this.valueMap.put(colmunname.toLowerCase(), value);
    }

    public Object getObject(String colmunname) {
        return this.valueMap.get(colmunname.toLowerCase());
    }

    public String getString(String colmunname) {
        return (String)this.valueMap.get(colmunname.toLowerCase());
    }

    public Integer getInt(String colmunname) {
        Object val = this.valueMap.get(colmunname.toLowerCase());
        return val != null?(val instanceof BigDecimal?Integer.valueOf(((BigDecimal)val).intValue()):(Integer)val):null;
    }

    public Map<String, Object> getValueMap() {
        return this.valueMap;
    }

    public String toString() {
        return this.valueMap.toString();
    }
}