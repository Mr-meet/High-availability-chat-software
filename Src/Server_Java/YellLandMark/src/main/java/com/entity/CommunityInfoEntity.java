package com.entity;

import javax.persistence.*;
import java.util.Arrays;

/**
 * Created by mrfj on 2017/5/5.
 */
@Entity
@Table(name = "community_info")
public class CommunityInfoEntity {
    private String communityId;
    private String communityName;
    private String communityType;
    private String communityUserMaxNumber;
    private String adminAccount;
    private byte[] icon;
    private String otherInfo;

    @Id
    @Column(name = "community_id")
    public String getCommunityId() {
        return communityId;
    }

    public void setCommunityId(String communityId) {
        this.communityId = communityId;
    }

    @Basic
    @Column(name = "community_name")
    public String getCommunityName() {
        return communityName;
    }

    public void setCommunityName(String communityName) {
        this.communityName = communityName;
    }

    @Basic
    @Column(name = "community_type")
    public String getCommunityType() {
        return communityType;
    }

    public void setCommunityType(String communityType) {
        this.communityType = communityType;
    }

    @Basic
    @Column(name = "community_user_max_number")
    public String getCommunityUserMaxNumber() {
        return communityUserMaxNumber;
    }

    public void setCommunityUserMaxNumber(String communityUserMaxNumber) {
        this.communityUserMaxNumber = communityUserMaxNumber;
    }

    @Basic
    @Column(name = "admin_account")
    public String getAdminAccount() {
        return adminAccount;
    }

    public void setAdminAccount(String adminAccount) {
        this.adminAccount = adminAccount;
    }

    @Basic
    @Column(name = "icon")
    public byte[] getIcon() {
        return icon;
    }

    public void setIcon(byte[] icon) {
        this.icon = icon;
    }

    @Basic
    @Column(name = "other_info")
    public String getOtherInfo() {
        return otherInfo;
    }

    public void setOtherInfo(String otherInfo) {
        this.otherInfo = otherInfo;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        CommunityInfoEntity that = (CommunityInfoEntity) o;

        if (communityId != null ? !communityId.equals(that.communityId) : that.communityId != null) return false;
        if (communityName != null ? !communityName.equals(that.communityName) : that.communityName != null)
            return false;
        if (communityType != null ? !communityType.equals(that.communityType) : that.communityType != null)
            return false;
        if (communityUserMaxNumber != null ? !communityUserMaxNumber.equals(that.communityUserMaxNumber) : that.communityUserMaxNumber != null)
            return false;
        if (adminAccount != null ? !adminAccount.equals(that.adminAccount) : that.adminAccount != null) return false;
        if (!Arrays.equals(icon, that.icon)) return false;
        if (otherInfo != null ? !otherInfo.equals(that.otherInfo) : that.otherInfo != null) return false;

        return true;
    }

    @Override
    public int hashCode() {
        int result = communityId != null ? communityId.hashCode() : 0;
        result = 31 * result + (communityName != null ? communityName.hashCode() : 0);
        result = 31 * result + (communityType != null ? communityType.hashCode() : 0);
        result = 31 * result + (communityUserMaxNumber != null ? communityUserMaxNumber.hashCode() : 0);
        result = 31 * result + (adminAccount != null ? adminAccount.hashCode() : 0);
        result = 31 * result + Arrays.hashCode(icon);
        result = 31 * result + (otherInfo != null ? otherInfo.hashCode() : 0);
        return result;
    }
}
