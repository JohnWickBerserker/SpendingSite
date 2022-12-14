-- Role: finadmin
-- DROP ROLE finadmin;

CREATE ROLE finadmin WITH
  LOGIN
  NOSUPERUSER
  INHERIT
  NOCREATEDB
  NOCREATEROLE
  NOREPLICATION
  PASSWORD '123';



-- SCHEMA: fin

-- DROP SCHEMA fin ;

CREATE SCHEMA fin
    AUTHORIZATION finadmin;



-- Table: fin.spendkinds

-- DROP TABLE fin.spendkinds;

CREATE TABLE fin.spendkinds
(
    spendkindid bigint NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    name character varying(2000) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT spendkinds_pkey PRIMARY KEY (spendkindid)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE fin.spendkinds
    OWNER to finadmin;



-- Table: fin.spendings

-- DROP TABLE fin.spendings;

CREATE TABLE fin.spendings
(
    spendid bigint NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    spendkindid bigint,
    amount numeric(12,2),
    note character varying(2000) COLLATE pg_catalog."default",
    spenddate date DEFAULT now(),
    CONSTRAINT spendings_pkey PRIMARY KEY (spendid),
    CONSTRAINT fk_spendings_spendkind FOREIGN KEY (spendkindid)
        REFERENCES fin.spendkinds (spendkindid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE fin.spendings
    OWNER to finadmin;

INSERT INTO fin.spendkinds(spendkindid, name)
VALUES(1, 'Food');

INSERT INTO fin.spendkinds(spendkindid, name)
VALUES(2, 'Household');

INSERT INTO fin.spendkinds(spendkindid, name)
VALUES(3, 'Car');

INSERT INTO fin.spendkinds(spendkindid, name)
VALUES(4, 'Cat');
