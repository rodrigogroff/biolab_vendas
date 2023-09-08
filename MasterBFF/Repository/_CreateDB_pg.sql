
CREATE TABLE IF NOT EXISTS public."I_Migration" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."I_Migration" ADD COLUMN if not exists "stTag" character varying(250);

CREATE TABLE IF NOT EXISTS public."Company" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."Company" OWNER to postgres;
ALTER TABLE public."Company" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."Company" ADD COLUMN if not exists "dtJoin" timestamp without time zone;
ALTER TABLE public."Company" ADD COLUMN if not exists "dtExpires" timestamp without time zone;

CREATE TABLE IF NOT EXISTS public."User" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."User" OWNER to postgres;
ALTER TABLE public."User" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."User" ADD COLUMN if not exists "stEmail" character varying(250);
ALTER TABLE public."User" ADD COLUMN if not exists "stPassword" character varying(250);
ALTER TABLE public."User" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "bAdmin" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "bManager" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."User" ADD COLUMN if not exists "stPhone" character varying(20);
ALTER TABLE public."User" ADD COLUMN if not exists "dtJoin" timestamp without time zone;
ALTER TABLE public."User" ADD COLUMN if not exists "dtLastLogin" timestamp without time zone;
ALTER TABLE public."User" ADD COLUMN if not exists "fkProfile" int;
ALTER TABLE public."User" ADD COLUMN if not exists "fkUserType" int;
ALTER TABLE public."User" ADD COLUMN if not exists "stUserPicture" character varying(500);

CREATE TABLE IF NOT EXISTS public."Squad" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."Squad" OWNER to postgres;
ALTER TABLE public."Squad" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."Squad" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."Squad" ADD COLUMN if not exists "stDescription" character varying(2500);
ALTER TABLE public."Squad" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."Squad" ADD COLUMN if not exists "dtJoin" timestamp without time zone;

CREATE TABLE IF NOT EXISTS public."Project" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."Project" OWNER to postgres;
ALTER TABLE public."Project" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."Project" ADD COLUMN if not exists "fkCompanyUnit" int;
ALTER TABLE public."Project" ADD COLUMN if not exists "stName" chara0cter varying(250);
ALTER TABLE public."Project" ADD COLUMN if not exists "dtJoin" timestamp without time zone;
ALTER TABLE public."Project" ADD COLUMN if not exists "stDescription" character varying(250);
ALTER TABLE public."Project" ADD COLUMN if not exists "bActive" boolean;

CREATE TABLE IF NOT EXISTS public."UserSquad" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserSquad" OWNER to postgres;
ALTER TABLE public."UserSquad" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserSquad" ADD COLUMN if not exists "fkSquad" int;
ALTER TABLE public."UserSquad" ADD COLUMN if not exists "fkCompany" int;

CREATE TABLE IF NOT EXISTS public."UserProject" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserProject" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserProject" ADD COLUMN if not exists "fkProject" int;
ALTER TABLE public."UserProject" ADD COLUMN if not exists "fkCompany" int;

CREATE TABLE IF NOT EXISTS public."SquadProject" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."SquadProject" OWNER to postgres;
ALTER TABLE public."SquadProject" ADD COLUMN if not exists "fkSquad" int;
ALTER TABLE public."SquadProject" ADD COLUMN if not exists "fkProject" int;
ALTER TABLE public."SquadProject" ADD COLUMN if not exists "fkCompany" int;

CREATE TABLE IF NOT EXISTS public."Client" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."Client" OWNER to postgres;
ALTER TABLE public."Client" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."Client" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."Client" ADD COLUMN if not exists "stDescription" character varying(250);
ALTER TABLE public."Client" ADD COLUMN if not exists "dtJoin" timestamp without time zone;
ALTER TABLE public."Client" ADD COLUMN if not exists "stAddress" character varying(250);
ALTER TABLE public."Client" ADD COLUMN if not exists "stAddressNumber" character varying(50);
ALTER TABLE public."Client" ADD COLUMN if not exists "stAddressComplement" character varying(50);
ALTER TABLE public."Client" ADD COLUMN if not exists "stCity" character varying(250);
ALTER TABLE public."Client" ADD COLUMN if not exists "stClientType" character varying(250);
ALTER TABLE public."Client" ADD COLUMN if not exists "stCountry" character varying(250);
ALTER TABLE public."Client" ADD COLUMN if not exists "stZipCode" character varying(20);
ALTER TABLE public."Client" ADD COLUMN if not exists "stPhone" character varying(20);
ALTER TABLE public."Client" ADD COLUMN if not exists "stSite" character varying(250);

CREATE TABLE IF NOT EXISTS public."ClientProject" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."ClientProject" OWNER to postgres;
ALTER TABLE public."ClientProject" ADD COLUMN if not exists "fkClient" int;
ALTER TABLE public."ClientProject" ADD COLUMN if not exists "fkProject" int;
ALTER TABLE public."ClientProject" ADD COLUMN if not exists "fkCompany" int;

CREATE TABLE IF NOT EXISTS public."UserProfile" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserProfile" OWNER to postgres;
ALTER TABLE public."UserProfile" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserProfile" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."UserProfile" ADD COLUMN if not exists "bActive" boolean;

CREATE TABLE IF NOT EXISTS public."UserProjectProfile" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserProjectProfile" OWNER to postgres;
ALTER TABLE public."UserProjectProfile" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserProjectProfile" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserProjectProfile" ADD COLUMN if not exists "fkProject" int;
ALTER TABLE public."UserProjectProfile" ADD COLUMN if not exists "fkProfile" int;

CREATE TABLE IF NOT EXISTS public."Contact" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."Contact" OWNER to postgres;
ALTER TABLE public."Contact" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."Contact" ADD COLUMN if not exists "fkClient" int;
ALTER TABLE public."Contact" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stEmail" character varying(250);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stPhone" character varying(20);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stObs" character varying(250);
ALTER TABLE public."Contact" ADD COLUMN if not exists "dtJoin" timestamp without time zone;

CREATE TABLE IF NOT EXISTS public."InteractionType" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."InteractionType" OWNER to postgres;
ALTER TABLE public."InteractionType" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."InteractionType" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."InteractionType" ADD COLUMN if not exists "stDescription" character varying(250);

CREATE TABLE IF NOT EXISTS public."ClientInteract" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."ClientInteract" OWNER to postgres;
ALTER TABLE public."ClientInteract" ADD COLUMN if not exists "fkClient" int;
ALTER TABLE public."ClientInteract" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."ClientInteract" ADD COLUMN if not exists "fkInteractionType" int;
ALTER TABLE public."ClientInteract" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."ClientInteract" ADD COLUMN if not exists "stObs" character varying(2500);
ALTER TABLE public."ClientInteract" ADD COLUMN if not exists "dtJoin" timestamp without time zone;

CREATE TABLE IF NOT EXISTS public."UserType" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserType" OWNER to postgres;
ALTER TABLE public."UserType" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserType" ADD COLUMN if not exists "stName" character varying(250);

CREATE TABLE IF NOT EXISTS public."CompanyUnit" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyUnit" OWNER to postgres;
ALTER TABLE public."CompanyUnit" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyUnit" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."CompanyUnit" ADD COLUMN if not exists "dtJoin" timestamp without time zone;

CREATE TABLE IF NOT EXISTS public."CompanyUnitPhone" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyUnitPhone" OWNER to postgres;
ALTER TABLE public."CompanyUnitPhone" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyUnitPhone" ADD COLUMN if not exists "fkCompanyUnit" int;
ALTER TABLE public."CompanyUnitPhone" ADD COLUMN if not exists "stPhone" character varying(50);
ALTER TABLE public."CompanyUnitPhone" ADD COLUMN if not exists "dtJoin" timestamp without time zone;

CREATE TABLE IF NOT EXISTS public."CompanyUnitAddress" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyUnitAddress" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyUnitAddress" ADD COLUMN if not exists "fkCompanyUnit" int;
ALTER TABLE public."CompanyUnitAddress" ADD COLUMN if not exists "stAddress" character varying(250);
ALTER TABLE public."CompanyUnitAddress" ADD COLUMN if not exists "stAddressNumber" character varying(50);
ALTER TABLE public."CompanyUnitAddress" ADD COLUMN if not exists "stAddressComplement" character varying(50);
ALTER TABLE public."CompanyUnitAddress" ADD COLUMN if not exists "stCity" character varying(250);
ALTER TABLE public."CompanyUnitAddress" ADD COLUMN if not exists "stCountry" character varying(250);
ALTER TABLE public."CompanyUnitAddress" ADD COLUMN if not exists "stZipCode" character varying(20);
