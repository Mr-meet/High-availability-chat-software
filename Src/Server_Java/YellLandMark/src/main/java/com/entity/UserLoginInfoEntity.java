package com.entity;

import javax.persistence.*;
import java.sql.Date;
import java.sql.Timestamp;

/**
 * Created by mrfj on 2017/5/5.
 */
@Entity
@Table(name = "user_login_info")
public class UserLoginInfoEntity {
    private String userNum;
    private String password;
    private Timestamp lastLoginTime;
    private Date registTime;

    @Id
    @Column(name = "user_num")
    public String getUserNum() {
        return userNum;
    }

    public void setUserNum(String userNum) {
        this.userNum = userNum;
    }

    @Basic
    @Column(name = "password")
    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    @Basic
    @Column(name = "last_login_time")
    public Timestamp getLastLoginTime() {
        return lastLoginTime;
    }

    public void setLastLoginTime(Timestamp lastLoginTime) {
        this.lastLoginTime = lastLoginTime;
    }

    @Basic
    @Column(name = "regist_time")
    public Date getRegistTime() {
        return registTime;
    }

    public void setRegistTime(Date registTime) {
        this.registTime = registTime;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        UserLoginInfoEntity that = (UserLoginInfoEntity) o;

        if (userNum != null ? !userNum.equals(that.userNum) : that.userNum != null) return false;
        if (password != null ? !password.equals(that.password) : that.password != null) return false;
        if (lastLoginTime != null ? !lastLoginTime.equals(that.lastLoginTime) : that.lastLoginTime != null)
            return false;
        if (registTime != null ? !registTime.equals(that.registTime) : that.registTime != null) return false;

        return true;
    }

    @Override
    public int hashCode() {
        int result = userNum != null ? userNum.hashCode() : 0;
        result = 31 * result + (password != null ? password.hashCode() : 0);
        result = 31 * result + (lastLoginTime != null ? lastLoginTime.hashCode() : 0);
        result = 31 * result + (registTime != null ? registTime.hashCode() : 0);
        return result;
    }
}
