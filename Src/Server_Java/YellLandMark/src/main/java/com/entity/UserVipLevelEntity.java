package com.entity;

import javax.persistence.*;
import java.sql.Date;

/**
 * Created by mrfj on 2017/5/5.
 */
@Entity
@Table(name = "user_vip_level")
public class UserVipLevelEntity {
    private String userNum;
    private byte vipFlag;
    private int useTimeCounter;
    private int vipTimeCounter;
    private Date vipOwenDay;
    private Integer dayCounter;

    @Id
    @Column(name = "user_num")
    public String getUserNum() {
        return userNum;
    }

    public void setUserNum(String userNum) {
        this.userNum = userNum;
    }

    @Basic
    @Column(name = "vip_flag")
    public byte getVipFlag() {
        return vipFlag;
    }

    public void setVipFlag(byte vipFlag) {
        this.vipFlag = vipFlag;
    }

    @Basic
    @Column(name = "use_time_counter")
    public int getUseTimeCounter() {
        return useTimeCounter;
    }

    public void setUseTimeCounter(int useTimeCounter) {
        this.useTimeCounter = useTimeCounter;
    }

    @Basic
    @Column(name = "vip_time_counter")
    public int getVipTimeCounter() {
        return vipTimeCounter;
    }

    public void setVipTimeCounter(int vipTimeCounter) {
        this.vipTimeCounter = vipTimeCounter;
    }

    @Basic
    @Column(name = "vip_owen_day")
    public Date getVipOwenDay() {
        return vipOwenDay;
    }

    public void setVipOwenDay(Date vipOwenDay) {
        this.vipOwenDay = vipOwenDay;
    }

    @Basic
    @Column(name = "day_counter")
    public Integer getDayCounter() {
        return dayCounter;
    }

    public void setDayCounter(Integer dayCounter) {
        this.dayCounter = dayCounter;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        UserVipLevelEntity that = (UserVipLevelEntity) o;

        if (vipFlag != that.vipFlag) return false;
        if (useTimeCounter != that.useTimeCounter) return false;
        if (vipTimeCounter != that.vipTimeCounter) return false;
        if (userNum != null ? !userNum.equals(that.userNum) : that.userNum != null) return false;
        if (vipOwenDay != null ? !vipOwenDay.equals(that.vipOwenDay) : that.vipOwenDay != null) return false;
        if (dayCounter != null ? !dayCounter.equals(that.dayCounter) : that.dayCounter != null) return false;

        return true;
    }

    @Override
    public int hashCode() {
        int result = userNum != null ? userNum.hashCode() : 0;
        result = 31 * result + (int) vipFlag;
        result = 31 * result + useTimeCounter;
        result = 31 * result + vipTimeCounter;
        result = 31 * result + (vipOwenDay != null ? vipOwenDay.hashCode() : 0);
        result = 31 * result + (dayCounter != null ? dayCounter.hashCode() : 0);
        return result;
    }
}
