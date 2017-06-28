package com.entity;

import javax.persistence.*;
import java.sql.Date;
import java.util.Arrays;

/**
 * Created by mrfj on 2017/5/5.
 */
@Entity
@Table(name = "user_info")
public class UserInfoEntity {
    private String userName;
    private String userNum;
    private String signatureOfPersonality;
    private String homeland;
    private Date birthday;
    private String email;
    private String career;
    private String placeOfResidence;
    private String gender;
    private byte[] image;
    private String homepage;
    private byte[] icon;

    @Basic
    @Column(name = "user_name")
    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    @Id
    @Column(name = "user_num")
    public String getUserNum() {
        return userNum;
    }

    public void setUserNum(String userNum) {
        this.userNum = userNum;
    }

    @Basic
    @Column(name = "signature_of_personality")
    public String getSignatureOfPersonality() {
        return signatureOfPersonality;
    }

    public void setSignatureOfPersonality(String signatureOfPersonality) {
        this.signatureOfPersonality = signatureOfPersonality;
    }

    @Basic
    @Column(name = "homeland")
    public String getHomeland() {
        return homeland;
    }

    public void setHomeland(String homeland) {
        this.homeland = homeland;
    }

    @Basic
    @Column(name = "birthday")
    public Date getBirthday() {
        return birthday;
    }

    public void setBirthday(Date birthday) {
        this.birthday = birthday;
    }

    @Basic
    @Column(name = "email")
    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    @Basic
    @Column(name = "career")
    public String getCareer() {
        return career;
    }

    public void setCareer(String career) {
        this.career = career;
    }

    @Basic
    @Column(name = "place_of_residence")
    public String getPlaceOfResidence() {
        return placeOfResidence;
    }

    public void setPlaceOfResidence(String placeOfResidence) {
        this.placeOfResidence = placeOfResidence;
    }

    @Basic
    @Column(name = "gender")
    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    @Basic
    @Column(name = "image")
    public byte[] getImage() {
        return image;
    }

    public void setImage(byte[] image) {
        this.image = image;
    }

    @Basic
    @Column(name = "homepage")
    public String getHomepage() {
        return homepage;
    }

    public void setHomepage(String homepage) {
        this.homepage = homepage;
    }

    @Basic
    @Column(name = "icon")
    public byte[] getIcon() {
        return icon;
    }

    public void setIcon(byte[] icon) {
        this.icon = icon;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        UserInfoEntity that = (UserInfoEntity) o;

        if (userName != null ? !userName.equals(that.userName) : that.userName != null) return false;
        if (userNum != null ? !userNum.equals(that.userNum) : that.userNum != null) return false;
        if (signatureOfPersonality != null ? !signatureOfPersonality.equals(that.signatureOfPersonality) : that.signatureOfPersonality != null)
            return false;
        if (homeland != null ? !homeland.equals(that.homeland) : that.homeland != null) return false;
        if (birthday != null ? !birthday.equals(that.birthday) : that.birthday != null) return false;
        if (email != null ? !email.equals(that.email) : that.email != null) return false;
        if (career != null ? !career.equals(that.career) : that.career != null) return false;
        if (placeOfResidence != null ? !placeOfResidence.equals(that.placeOfResidence) : that.placeOfResidence != null)
            return false;
        if (gender != null ? !gender.equals(that.gender) : that.gender != null) return false;
        if (!Arrays.equals(image, that.image)) return false;
        if (homepage != null ? !homepage.equals(that.homepage) : that.homepage != null) return false;
        if (!Arrays.equals(icon, that.icon)) return false;

        return true;
    }

    @Override
    public int hashCode() {
        int result = userName != null ? userName.hashCode() : 0;
        result = 31 * result + (userNum != null ? userNum.hashCode() : 0);
        result = 31 * result + (signatureOfPersonality != null ? signatureOfPersonality.hashCode() : 0);
        result = 31 * result + (homeland != null ? homeland.hashCode() : 0);
        result = 31 * result + (birthday != null ? birthday.hashCode() : 0);
        result = 31 * result + (email != null ? email.hashCode() : 0);
        result = 31 * result + (career != null ? career.hashCode() : 0);
        result = 31 * result + (placeOfResidence != null ? placeOfResidence.hashCode() : 0);
        result = 31 * result + (gender != null ? gender.hashCode() : 0);
        result = 31 * result + Arrays.hashCode(image);
        result = 31 * result + (homepage != null ? homepage.hashCode() : 0);
        result = 31 * result + Arrays.hashCode(icon);
        return result;
    }
}
