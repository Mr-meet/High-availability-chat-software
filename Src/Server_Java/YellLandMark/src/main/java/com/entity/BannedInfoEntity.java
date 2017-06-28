package com.entity;

import javax.persistence.*;
import java.sql.Timestamp;

/**
 * Created by mrfj on 2017/5/5.
 */
@Entity
@Table(name = "banned_info")
public class BannedInfoEntity {
    private Byte disable;
    private String uuid;
    private String userNum;
    private Timestamp timeLine;
    private long timeLong;

    @Basic
    @Column(name = "disable")
    public Byte getDisable() {
        return disable;
    }

    public void setDisable(Byte disable) {
        this.disable = disable;
    }

    @Id
    @Column(name = "uuid")
    public String getUuid() {
        return uuid;
    }

    public void setUuid(String uuid) {
        this.uuid = uuid;
    }

    @Basic
    @Column(name = "user_num")
    public String getUserNum() {
        return userNum;
    }

    public void setUserNum(String userNum) {
        this.userNum = userNum;
    }

    @Basic
    @Column(name = "time_line")
    public Timestamp getTimeLine() {
        return timeLine;
    }

    public void setTimeLine(Timestamp timeLine) {
        this.timeLine = timeLine;
    }

    @Basic
    @Column(name = "time_long")
    public long getTimeLong() {
        return timeLong;
    }

    public void setTimeLong(long timeLong) {
        this.timeLong = timeLong;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        BannedInfoEntity that = (BannedInfoEntity) o;

        if (timeLong != that.timeLong) return false;
        if (disable != null ? !disable.equals(that.disable) : that.disable != null) return false;
        if (uuid != null ? !uuid.equals(that.uuid) : that.uuid != null) return false;
        if (userNum != null ? !userNum.equals(that.userNum) : that.userNum != null) return false;
        if (timeLine != null ? !timeLine.equals(that.timeLine) : that.timeLine != null) return false;

        return true;
    }

    @Override
    public int hashCode() {
        int result = disable != null ? disable.hashCode() : 0;
        result = 31 * result + (uuid != null ? uuid.hashCode() : 0);
        result = 31 * result + (userNum != null ? userNum.hashCode() : 0);
        result = 31 * result + (timeLine != null ? timeLine.hashCode() : 0);
        result = 31 * result + (int) (timeLong ^ (timeLong >>> 32));
        return result;
    }
}
