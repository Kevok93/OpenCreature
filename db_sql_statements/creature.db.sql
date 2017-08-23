CREATE TABLE type_bonus (
	`atk_id` tinyint,
	`def_id` tinyint,
	`bonus` decimal(2,1),
	constraint `pk_type_bonus` primary key (`atk_id`,`def_id`) on conflict rollback
);
CREATE TABLE level_moves (
	`poke_id` int,
	`move_id` int,
	`level` tinyint
);
CREATE TABLE types (
	`id` tinyint,
	`name` varchar(16),
	constraint `pk_types` primary key (`id`) on conflict rollback
);
CREATE TABLE effects (
	`id` tinyint,
	`name` varchar(16),
	constraint pk_effects primary key (`id`) on conflict rollback
);
CREATE TABLE abilities (
	`id` smallint,
	`name` varchar(16),
	`description` varchar(128),
	constraint pk_abilities primary key (`id`) on conflict rollback
);
CREATE TABLE moves (
	`id` int,
	`name` varchar(16),
	`type` tinyint,
	`power` tinyint,
	`accuracy` tinyint,
	`pp` tinyint,
	`effect_id` smallint,
	`affinity` tinyint,
	`misc_info` blob(2),
	`description` varchar(128),
	constraint `pk_moves` primary key (`id`) on conflict rollback
);
CREATE TABLE "species" (
    `id`    int,
    `name`  varchar(16),
    `ability1`  smallint,
    `ability2`  smallint,
    `gender_ratio`  tinyint,
    `type1` tinyint,
    `type2` tinyint,
    `capture_rate`  tinyint,
    `max_exp`   int,
    `ev_type`   tinyint,
    `ev_val`    tinyint,
    `base_atk`  smallint,
    `base_def`  smallint,
    `base_spatk`    smallint,
    `base_spdef`    smallint,
    `base_hp`   smallint,
    `base_speed`    smallint,
    `max_atk`   smallint,
    `max_def`   smallint,
    `max_spatk` smallint,
    `max_spdef` smallint,
    `max_hp`    smallint,
    `max_speed` smallint,
    `tm_list`   blob(16),
    PRIMARY KEY(`id`)

);
CREATE TABLE natures (
	`id` tinyint,
	`name` varchar(16),
	`atk` tinyint,
	`def` tinyint,
	`spatk` tinyint,
	`spdef` tinyint,
	`speed` tinyint,
	constraint `pk_natures` primary key (`id`) on conflict rollback
);

