/*==============================================================*/
/* DBMS name:      MySQL 5.0                                    */
/* Created on:     2017/4/24 4:54:11                            */
/*==============================================================*/


drop table if exists banned_info;

drop table if exists community_info;

drop table if exists dictionary;

drop table if exists program_debug;

drop table if exists user_info;

drop table if exists user_login_info;

drop table if exists user_vip_level;

/*==============================================================*/
/* Table: banned_info                                           */
/*==============================================================*/
create table banned_info
(
   disable              bool,
   uuid                 varchar(36) not null,
   user_num             varchar(12) not null,
   time_line            datetime not null,
   time_long            bigint not null,
   primary key (uuid)
);

/*==============================================================*/
/* Table: community_info                                        */
/*==============================================================*/
create table community_info
(
   community_id         varchar(12) not null,
   community_name       varchar(20) not null,
   community_type       varchar(36) not null,
   community_user_max_number varchar(36) not null default '50',
   admin_account        varchar(12) not null,
   icon                 mediumblob,
   other_info           varchar(255),
   primary key (community_id)
);

/*==============================================================*/
/* Table: dictionary                                            */
/*==============================================================*/
create table dictionary
(
   uuid                 varchar(36) not null,
   value                varchar(80) not null,
   name                 varchar(40) not null,
   useage               varchar(20),
   demo                 varchar(120),
   primary key (uuid)
);

/*==============================================================*/
/* Table: program_debug                                         */
/*==============================================================*/
create table program_debug
(
   uuid                 varchar(36) not null,
   platform             varchar(36) not null,
   time                 datetime not null,
   type                 varchar(36) not null,
   message              varchar(2048),
   primary key (uuid)
);

/*==============================================================*/
/* Table: user_info                                             */
/*==============================================================*/
create table user_info
(
   user_name            varchar(20) not null,
   user_num             varchar(12) not null,
   signature_of_personality varchar(255),
   homeland             varchar(36),
   birthday             date,
   email                varchar(40),
   career               varchar(36),
   place_of_residence   varchar(36),
   gender               varchar(36),
   image                mediumblob,
   homepage             varchar(120),
   icon                 mediumblob,
   primary key (user_num)
);

/*==============================================================*/
/* Table: user_login_info                                       */
/*==============================================================*/
create table user_login_info
(
   user_num             varchar(12) not null,
   password             varchar(12),
   last_login_time      datetime,
   regist_time          date,
   primary key (user_num)
);

/*==============================================================*/
/* Table: user_vip_level                                        */
/*==============================================================*/
create table user_vip_level
(
   user_num             varchar(12) not null,
   vip_flag             bool not null default false,
   use_time_counter     int not null default 0,
   vip_time_counter     int not null default 0,
   vip_owen_day         date,
   day_counter          int,
   primary key (user_num)
);

alter table banned_info add constraint FK_user_num_ban foreign key (user_num)
      references user_login_info (user_num) on delete restrict on update restrict;

alter table community_info add constraint FK_admin_account_num foreign key (admin_account)
      references user_login_info (user_num) on delete restrict on update restrict;

alter table community_info add constraint FK_comunity_type foreign key (community_type)
      references dictionary (uuid) on delete restrict on update restrict;

alter table community_info add constraint FK_max_community_count_number foreign key (community_user_max_number)
      references dictionary (uuid) on delete restrict on update restrict;

alter table program_debug add constraint FK_debug_type foreign key (type)
      references dictionary (uuid) on delete restrict on update restrict;

alter table program_debug add constraint FK_platform foreign key (platform)
      references dictionary (uuid) on delete restrict on update restrict;

alter table user_info add constraint FK_career foreign key (career)
      references dictionary (uuid) on delete restrict on update restrict;

alter table user_info add constraint FK_gemder foreign key (gender)
      references dictionary (uuid) on delete restrict on update restrict;

alter table user_info add constraint FK_homeland foreign key (homeland)
      references dictionary (uuid) on delete restrict on update restrict;

alter table user_info add constraint FK_place_now foreign key (place_of_residence)
      references dictionary (uuid) on delete restrict on update restrict;

alter table user_info add constraint FK_user_num_info foreign key (user_num)
      references user_login_info (user_num) on delete restrict on update restrict;

alter table user_vip_level add constraint FK_user_num_vip foreign key (user_num)
      references user_login_info (user_num) on delete restrict on update restrict;

