--
-- PostgreSQL database dump
--

-- Dumped from database version 14.9 (Ubuntu 14.9-0ubuntu0.22.04.1)
-- Dumped by pg_dump version 16.0 (Ubuntu 16.0-1.pgdg22.04+1)

-- Started on 2023-10-05 17:26:19 +0530

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

-- *not* creating schema, since initdb creates it


ALTER SCHEMA public OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 225 (class 1259 OID 17172)
-- Name: AddOn; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AddOn" (
    "Id" bigint NOT NULL,
    "Name" text NOT NULL,
    "Image" text NOT NULL,
    "Description" text NOT NULL,
    "ValidNoOfDays" integer NOT NULL,
    "ChargePerGb" real NOT NULL,
    "DataAmount" real NOT NULL,
    "AddOnId" bigint,
    "Type" integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."AddOn" OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 17115)
-- Name: AddOnActivation; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AddOnActivation" (
    "Id" bigint NOT NULL,
    "DataServiceId" bigint NOT NULL,
    "UserId" bigint NOT NULL,
    "AddOnId" bigint NOT NULL,
    "ActivatedDateTime" timestamp with time zone NOT NULL,
    "DataUsage" double precision NOT NULL,
    "TotalData" double precision DEFAULT 0 NOT NULL,
    "ExpireDateTime" timestamp with time zone DEFAULT '-infinity'::timestamp with time zone NOT NULL,
    "Type" integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."AddOnActivation" OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 17114)
-- Name: AddOnActivation_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."AddOnActivation_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."AddOnActivation_Id_seq" OWNER TO postgres;

--
-- TOC entry 3487 (class 0 OID 0)
-- Dependencies: 210
-- Name: AddOnActivation_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."AddOnActivation_Id_seq" OWNED BY public."AddOnActivation"."Id";


--
-- TOC entry 224 (class 1259 OID 17171)
-- Name: AddOn_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."AddOn_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."AddOn_Id_seq" OWNER TO postgres;

--
-- TOC entry 3488 (class 0 OID 0)
-- Dependencies: 224
-- Name: AddOn_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."AddOn_Id_seq" OWNED BY public."AddOn"."Id";


--
-- TOC entry 227 (class 1259 OID 17186)
-- Name: Bill; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Bill" (
    "Id" bigint NOT NULL,
    "UserId" bigint NOT NULL,
    "ServiceId" bigint NOT NULL,
    "Month" integer NOT NULL,
    "Year" integer NOT NULL,
    "TaxAmount" double precision NOT NULL,
    "TotalAmount" double precision NOT NULL,
    "DueAmount" double precision NOT NULL,
    "BillId" bigint
);


ALTER TABLE public."Bill" OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 17185)
-- Name: Bill_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Bill_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Bill_Id_seq" OWNER TO postgres;

--
-- TOC entry 3489 (class 0 OID 0)
-- Dependencies: 226
-- Name: Bill_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Bill_Id_seq" OWNED BY public."Bill"."Id";


--
-- TOC entry 213 (class 1259 OID 17122)
-- Name: DataService; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."DataService" (
    "UserId" bigint NOT NULL,
    "IsDataRoaming" integer NOT NULL,
    "DataRoamingCharge" real NOT NULL
);


ALTER TABLE public."DataService" OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 17121)
-- Name: DataService_UserId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."DataService_UserId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."DataService_UserId_seq" OWNER TO postgres;

--
-- TOC entry 3490 (class 0 OID 0)
-- Dependencies: 212
-- Name: DataService_UserId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."DataService_UserId_seq" OWNED BY public."DataService"."UserId";


--
-- TOC entry 215 (class 1259 OID 17129)
-- Name: Notification; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Notification" (
    "Id" bigint NOT NULL,
    "UserId" bigint NOT NULL,
    "DateTime" timestamp with time zone NOT NULL,
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "Priority" integer NOT NULL
);


ALTER TABLE public."Notification" OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 17128)
-- Name: Notification_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Notification_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Notification_Id_seq" OWNER TO postgres;

--
-- TOC entry 3491 (class 0 OID 0)
-- Dependencies: 214
-- Name: Notification_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Notification_Id_seq" OWNED BY public."Notification"."Id";


--
-- TOC entry 217 (class 1259 OID 17138)
-- Name: Package; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Package" (
    "Id" bigint NOT NULL,
    "Name" text NOT NULL,
    "Renewal" integer,
    "Type" integer NOT NULL,
    "Description" text NOT NULL,
    "Image" text NOT NULL,
    "Charge" real NOT NULL,
    "OffPeekData" real NOT NULL,
    "PeekData" real NOT NULL,
    "AnytimeData" real NOT NULL,
    "S2SCallMins" integer NOT NULL,
    "S2SSmsCount" integer NOT NULL,
    "AnyNetCallMins" integer NOT NULL,
    "AnyNetSmsCount" integer NOT NULL
);


ALTER TABLE public."Package" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 17147)
-- Name: PackageUsage; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."PackageUsage" (
    "Id" bigint NOT NULL,
    "UserId" bigint NOT NULL,
    "ServiceId" bigint NOT NULL,
    "Year" integer NOT NULL,
    "Month" integer NOT NULL,
    "PackageId" bigint NOT NULL,
    "UpdateDateTime" timestamp with time zone NOT NULL,
    "OffPeekDataUsage" real NOT NULL,
    "PeekDataUsage" real NOT NULL,
    "AnytimeDataUsage" real NOT NULL,
    "S2SCallMinsUsage" integer NOT NULL,
    "S2SSmsCountUsage" integer NOT NULL,
    "AnyNetCallMinsUsage" integer NOT NULL,
    "AnyNetSmsCountUsage" integer NOT NULL,
    "State" integer NOT NULL
);


ALTER TABLE public."PackageUsage" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 17146)
-- Name: PackageUsage_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."PackageUsage_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."PackageUsage_Id_seq" OWNER TO postgres;

--
-- TOC entry 3492 (class 0 OID 0)
-- Dependencies: 218
-- Name: PackageUsage_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."PackageUsage_Id_seq" OWNED BY public."PackageUsage"."Id";


--
-- TOC entry 216 (class 1259 OID 17137)
-- Name: Package_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Package_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Package_Id_seq" OWNER TO postgres;

--
-- TOC entry 3493 (class 0 OID 0)
-- Dependencies: 216
-- Name: Package_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Package_Id_seq" OWNED BY public."Package"."Id";


--
-- TOC entry 221 (class 1259 OID 17154)
-- Name: Payment; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Payment" (
    "Id" bigint NOT NULL,
    "BillId" bigint NOT NULL,
    "PayDateTime" timestamp with time zone NOT NULL,
    "UserId" bigint NOT NULL,
    "ServiceId" bigint NOT NULL,
    "PayMethod" integer NOT NULL,
    "PayAmount" double precision NOT NULL,
    "Outstanding" double precision DEFAULT 0.0 NOT NULL
);


ALTER TABLE public."Payment" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 17153)
-- Name: Payment_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Payment_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Payment_Id_seq" OWNER TO postgres;

--
-- TOC entry 3494 (class 0 OID 0)
-- Dependencies: 220
-- Name: Payment_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Payment_Id_seq" OWNED BY public."Payment"."Id";


--
-- TOC entry 229 (class 1259 OID 17198)
-- Name: Service; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Service" (
    "Id" bigint NOT NULL,
    "Name" text,
    "Charge" real NOT NULL,
    "State" text,
    "Type" integer NOT NULL,
    "DataServiceId" bigint,
    "ServiceId" bigint
);


ALTER TABLE public."Service" OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 17197)
-- Name: Service_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Service_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Service_Id_seq" OWNER TO postgres;

--
-- TOC entry 3495 (class 0 OID 0)
-- Dependencies: 228
-- Name: Service_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Service_Id_seq" OWNED BY public."Service"."Id";


--
-- TOC entry 231 (class 1259 OID 17227)
-- Name: User; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."User" (
    "Id" bigint NOT NULL,
    "Nic" text NOT NULL,
    "FirstName" text NOT NULL,
    "LastName" text NOT NULL,
    "MobileNumber" text NOT NULL,
    "Email" text NOT NULL,
    "Password" text NOT NULL,
    "PastPassword" text[],
    "UserId" bigint
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 17226)
-- Name: User_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."User_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."User_Id_seq" OWNER TO postgres;

--
-- TOC entry 3496 (class 0 OID 0)
-- Dependencies: 230
-- Name: User_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."User_Id_seq" OWNED BY public."User"."Id";


--
-- TOC entry 223 (class 1259 OID 17163)
-- Name: VoiceService; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."VoiceService" (
    "UserId" bigint NOT NULL,
    "IsRingingTone" integer NOT NULL,
    "RingingToneName" text NOT NULL,
    "RingingToneCharge" real NOT NULL,
    "IsVoiceRoaming" integer NOT NULL,
    "VoiceRoamingCharge" real NOT NULL
);


ALTER TABLE public."VoiceService" OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 17162)
-- Name: VoiceService_UserId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."VoiceService_UserId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."VoiceService_UserId_seq" OWNER TO postgres;

--
-- TOC entry 3497 (class 0 OID 0)
-- Dependencies: 222
-- Name: VoiceService_UserId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."VoiceService_UserId_seq" OWNED BY public."VoiceService"."UserId";


--
-- TOC entry 209 (class 1259 OID 17109)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 3272 (class 2604 OID 17175)
-- Name: AddOn Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AddOn" ALTER COLUMN "Id" SET DEFAULT nextval('public."AddOn_Id_seq"'::regclass);


--
-- TOC entry 3261 (class 2604 OID 17118)
-- Name: AddOnActivation Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AddOnActivation" ALTER COLUMN "Id" SET DEFAULT nextval('public."AddOnActivation_Id_seq"'::regclass);


--
-- TOC entry 3274 (class 2604 OID 17189)
-- Name: Bill Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Bill" ALTER COLUMN "Id" SET DEFAULT nextval('public."Bill_Id_seq"'::regclass);


--
-- TOC entry 3265 (class 2604 OID 17125)
-- Name: DataService UserId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DataService" ALTER COLUMN "UserId" SET DEFAULT nextval('public."DataService_UserId_seq"'::regclass);


--
-- TOC entry 3266 (class 2604 OID 17132)
-- Name: Notification Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Notification" ALTER COLUMN "Id" SET DEFAULT nextval('public."Notification_Id_seq"'::regclass);


--
-- TOC entry 3267 (class 2604 OID 17141)
-- Name: Package Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Package" ALTER COLUMN "Id" SET DEFAULT nextval('public."Package_Id_seq"'::regclass);


--
-- TOC entry 3268 (class 2604 OID 17150)
-- Name: PackageUsage Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PackageUsage" ALTER COLUMN "Id" SET DEFAULT nextval('public."PackageUsage_Id_seq"'::regclass);


--
-- TOC entry 3269 (class 2604 OID 17157)
-- Name: Payment Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Payment" ALTER COLUMN "Id" SET DEFAULT nextval('public."Payment_Id_seq"'::regclass);


--
-- TOC entry 3275 (class 2604 OID 17201)
-- Name: Service Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Service" ALTER COLUMN "Id" SET DEFAULT nextval('public."Service_Id_seq"'::regclass);


--
-- TOC entry 3276 (class 2604 OID 17230)
-- Name: User Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User" ALTER COLUMN "Id" SET DEFAULT nextval('public."User_Id_seq"'::regclass);


--
-- TOC entry 3271 (class 2604 OID 17166)
-- Name: VoiceService UserId; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."VoiceService" ALTER COLUMN "UserId" SET DEFAULT nextval('public."VoiceService_UserId_seq"'::regclass);


--
-- TOC entry 3474 (class 0 OID 17172)
-- Dependencies: 225
-- Data for Name: AddOn; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AddOn" ("Id", "Name", "Image", "Description", "ValidNoOfDays", "ChargePerGb", "DataAmount", "AddOnId", "Type") FROM stdin;
1	1			30	100	1	\N	0
2	2			30	100	2	\N	0
3	3			30	100	3	\N	0
4	6			60	85	6	\N	0
5	15			60	75	15	\N	0
6	20			60	75	20	\N	0
8	YouTube	youtube.png	Unlock YouTube's Best	30	15	30	\N	1
7	Zoom Learn	zoomlearn.png	Empower Your Mind	30	10	15	\N	1
\.


--
-- TOC entry 3460 (class 0 OID 17115)
-- Dependencies: 211
-- Data for Name: AddOnActivation; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AddOnActivation" ("Id", "DataServiceId", "UserId", "AddOnId", "ActivatedDateTime", "DataUsage", "TotalData", "ExpireDateTime", "Type") FROM stdin;
6	3	1	8	2023-09-30 19:22:31.017127+05:30	0	30	2023-10-30 19:22:31.017133+05:30	1
3	3	1	1	2023-10-03 23:27:12.014441+05:30	0	4	2023-11-02 23:27:12.014444+05:30	0
4	3	1	4	2023-10-03 23:27:12.014446+05:30	0	12	2023-11-02 23:27:12.014446+05:30	0
8	3	1	6	2023-10-03 23:27:12.014446+05:30	0	20	2023-11-02 23:27:12.014446+05:30	0
9	3	1	2	2023-10-03 23:27:11.975142+05:30	0	2	2023-11-02 23:27:11.975147+05:30	0
7	3	1	7	2023-10-03 23:27:17.127625+05:30	0	45	2023-11-02 23:27:17.127627+05:30	1
\.


--
-- TOC entry 3476 (class 0 OID 17186)
-- Dependencies: 227
-- Data for Name: Bill; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Bill" ("Id", "UserId", "ServiceId", "Month", "Year", "TaxAmount", "TotalAmount", "DueAmount", "BillId") FROM stdin;
8	1	3	9	2023	279	5580	0	\N
11	1	4	9	2023	12.5	250	0	\N
\.


--
-- TOC entry 3462 (class 0 OID 17122)
-- Dependencies: 213
-- Data for Name: DataService; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."DataService" ("UserId", "IsDataRoaming", "DataRoamingCharge") FROM stdin;
1	1	150
\.


--
-- TOC entry 3464 (class 0 OID 17129)
-- Dependencies: 215
-- Data for Name: Notification; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Notification" ("Id", "UserId", "DateTime", "Title", "Description", "Priority") FROM stdin;
\.


--
-- TOC entry 3466 (class 0 OID 17138)
-- Dependencies: 217
-- Data for Name: Package; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Package" ("Id", "Name", "Renewal", "Type", "Description", "Image", "Charge", "OffPeekData", "PeekData", "AnytimeData", "S2SCallMins", "S2SSmsCount", "AnyNetCallMins", "AnyNetSmsCount") FROM stdin;
1	Web Starter	0	0	Experience unlimited connectivity	webstarter.png	1760	60	40	0	0	0	0	0
2	Web Family	0	0	Experience unlimited connectivity	webfamily.png	1990	70	50	0	0	0	0	0
3	Web Pro	0	0	Experience unlimited connectivity	webpro.png	2990	100	60	0	0	0	0	0
4	Web Ultimate	0	0	Experience unlimited connectivity	webultimate.png	5990	250	150	0	0	0	0	0
5	VoicePlus	0	1	Your words, our clarity.	voiceplus.png	499	0	0	0	500	0	500	0
6	TalkMaster	0	1	Your words, our clarity.	talkmaster.png	999	0	0	0	1000	0	1000	0
7	VoicePro	0	1	Your words, our clarity.	voicepro.png	1999	0	0	0	2000	0	2000	0
\.


--
-- TOC entry 3468 (class 0 OID 17147)
-- Dependencies: 219
-- Data for Name: PackageUsage; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."PackageUsage" ("Id", "UserId", "ServiceId", "Year", "Month", "PackageId", "UpdateDateTime", "OffPeekDataUsage", "PeekDataUsage", "AnytimeDataUsage", "S2SCallMinsUsage", "S2SSmsCountUsage", "AnyNetCallMinsUsage", "AnyNetSmsCountUsage", "State") FROM stdin;
10	1	3	2023	10	2	2023-10-03 22:47:28.230222+05:30	5	35	0	0	0	0	0	1
11	1	4	2023	10	6	2023-10-03 23:36:37.18318+05:30	0	0	0	60	0	0	0	1
8	1	3	2023	9	1	2023-09-29 01:59:10.98332+05:30	0	0	0	0	0	0	0	1
9	1	4	2023	9	5	2023-09-29 16:50:33.840123+05:30	0	0	0	0	0	0	0	0
\.


--
-- TOC entry 3470 (class 0 OID 17154)
-- Dependencies: 221
-- Data for Name: Payment; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Payment" ("Id", "BillId", "PayDateTime", "UserId", "ServiceId", "PayMethod", "PayAmount", "Outstanding") FROM stdin;
3	8	2023-10-03 21:00:09.737539+05:30	1	3	0	2460	5859
4	8	2023-10-03 21:00:31.478509+05:30	1	3	0	1600	3399
14	11	2023-10-03 22:03:15.447417+05:30	1	4	0	100	262.5
15	11	2023-10-04 14:30:49.860943+05:30	1	4	0	50	162.5
16	11	2023-10-04 14:35:35.464364+05:30	1	4	0	100	112.5
\.


--
-- TOC entry 3478 (class 0 OID 17198)
-- Dependencies: 229
-- Data for Name: Service; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Service" ("Id", "Name", "Charge", "State", "Type", "DataServiceId", "ServiceId") FROM stdin;
3	Internet Service	100	active	0	\N	\N
4	Teliphone Service	50	active	1	\N	\N
\.


--
-- TOC entry 3480 (class 0 OID 17227)
-- Dependencies: 231
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."User" ("Id", "Nic", "FirstName", "LastName", "MobileNumber", "Email", "Password", "PastPassword", "UserId") FROM stdin;
1	200023003227	Dinuka	Amarasinghe	0714872852	dinuka@gmail.com	$2a$11$PsQx2KKsFFYJdtDKBu9ALudSt3GNiCzMXfGFbj9JOVm2l56ebCFHu	\N	\N
2	200066701573	Sasangi	Nayanathara	0714872852	sasangi@gmail.com	$2a$11$VLKlXt.T1AOuRd9WlE2Q7OEvkyiYeCdYtkT99W7QYBBerwBheZD4q	\N	\N
\.


--
-- TOC entry 3472 (class 0 OID 17163)
-- Dependencies: 223
-- Data for Name: VoiceService; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."VoiceService" ("UserId", "IsRingingTone", "RingingToneName", "RingingToneCharge", "IsVoiceRoaming", "VoiceRoamingCharge") FROM stdin;
1	1	All Falls Down - Alan Walker	100	1	100
\.


--
-- TOC entry 3458 (class 0 OID 17109)
-- Dependencies: 209
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20230930065515_initial	7.0.11
20230930113410_initial2	7.0.11
20230930120524_initial3	7.0.11
20230930134710_initial4	7.0.11
20231003121357_initial5	7.0.11
20231003150058_initial6	7.0.11
20231004071651_initial7	7.0.11
\.


--
-- TOC entry 3498 (class 0 OID 0)
-- Dependencies: 210
-- Name: AddOnActivation_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."AddOnActivation_Id_seq"', 9, true);


--
-- TOC entry 3499 (class 0 OID 0)
-- Dependencies: 224
-- Name: AddOn_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."AddOn_Id_seq"', 8, true);


--
-- TOC entry 3500 (class 0 OID 0)
-- Dependencies: 226
-- Name: Bill_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Bill_Id_seq"', 11, true);


--
-- TOC entry 3501 (class 0 OID 0)
-- Dependencies: 212
-- Name: DataService_UserId_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."DataService_UserId_seq"', 1, true);


--
-- TOC entry 3502 (class 0 OID 0)
-- Dependencies: 214
-- Name: Notification_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Notification_Id_seq"', 1, false);


--
-- TOC entry 3503 (class 0 OID 0)
-- Dependencies: 218
-- Name: PackageUsage_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."PackageUsage_Id_seq"', 11, true);


--
-- TOC entry 3504 (class 0 OID 0)
-- Dependencies: 216
-- Name: Package_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Package_Id_seq"', 7, true);


--
-- TOC entry 3505 (class 0 OID 0)
-- Dependencies: 220
-- Name: Payment_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Payment_Id_seq"', 16, true);


--
-- TOC entry 3506 (class 0 OID 0)
-- Dependencies: 228
-- Name: Service_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Service_Id_seq"', 4, true);


--
-- TOC entry 3507 (class 0 OID 0)
-- Dependencies: 230
-- Name: User_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."User_Id_seq"', 2, true);


--
-- TOC entry 3508 (class 0 OID 0)
-- Dependencies: 222
-- Name: VoiceService_UserId_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."VoiceService_UserId_seq"', 1, true);


--
-- TOC entry 3295 (class 2606 OID 17179)
-- Name: AddOn PK_AddOn; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AddOn"
    ADD CONSTRAINT "PK_AddOn" PRIMARY KEY ("Id");


--
-- TOC entry 3280 (class 2606 OID 17120)
-- Name: AddOnActivation PK_AddOnActivation; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AddOnActivation"
    ADD CONSTRAINT "PK_AddOnActivation" PRIMARY KEY ("Id");


--
-- TOC entry 3298 (class 2606 OID 17191)
-- Name: Bill PK_Bill; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Bill"
    ADD CONSTRAINT "PK_Bill" PRIMARY KEY ("Id");


--
-- TOC entry 3282 (class 2606 OID 17127)
-- Name: DataService PK_DataService; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DataService"
    ADD CONSTRAINT "PK_DataService" PRIMARY KEY ("UserId");


--
-- TOC entry 3284 (class 2606 OID 17136)
-- Name: Notification PK_Notification; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Notification"
    ADD CONSTRAINT "PK_Notification" PRIMARY KEY ("Id");


--
-- TOC entry 3286 (class 2606 OID 17145)
-- Name: Package PK_Package; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Package"
    ADD CONSTRAINT "PK_Package" PRIMARY KEY ("Id");


--
-- TOC entry 3288 (class 2606 OID 17152)
-- Name: PackageUsage PK_PackageUsage; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."PackageUsage"
    ADD CONSTRAINT "PK_PackageUsage" PRIMARY KEY ("Id");


--
-- TOC entry 3290 (class 2606 OID 17161)
-- Name: Payment PK_Payment; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Payment"
    ADD CONSTRAINT "PK_Payment" PRIMARY KEY ("Id");


--
-- TOC entry 3302 (class 2606 OID 17205)
-- Name: Service PK_Service; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Service"
    ADD CONSTRAINT "PK_Service" PRIMARY KEY ("Id");


--
-- TOC entry 3305 (class 2606 OID 17234)
-- Name: User PK_User; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "PK_User" PRIMARY KEY ("Id");


--
-- TOC entry 3292 (class 2606 OID 17170)
-- Name: VoiceService PK_VoiceService; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."VoiceService"
    ADD CONSTRAINT "PK_VoiceService" PRIMARY KEY ("UserId");


--
-- TOC entry 3278 (class 2606 OID 17113)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 3293 (class 1259 OID 17270)
-- Name: IX_AddOn_AddOnId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AddOn_AddOnId" ON public."AddOn" USING btree ("AddOnId");


--
-- TOC entry 3296 (class 1259 OID 17271)
-- Name: IX_Bill_BillId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Bill_BillId" ON public."Bill" USING btree ("BillId");


--
-- TOC entry 3299 (class 1259 OID 17272)
-- Name: IX_Service_DataServiceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Service_DataServiceId" ON public."Service" USING btree ("DataServiceId");


--
-- TOC entry 3300 (class 1259 OID 17273)
-- Name: IX_Service_ServiceId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Service_ServiceId" ON public."Service" USING btree ("ServiceId");


--
-- TOC entry 3303 (class 1259 OID 17274)
-- Name: IX_User_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_User_UserId" ON public."User" USING btree ("UserId");


--
-- TOC entry 3306 (class 2606 OID 17180)
-- Name: AddOn FK_AddOn_AddOnActivation_AddOnId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AddOn"
    ADD CONSTRAINT "FK_AddOn_AddOnActivation_AddOnId" FOREIGN KEY ("AddOnId") REFERENCES public."AddOnActivation"("Id");


--
-- TOC entry 3307 (class 2606 OID 17192)
-- Name: Bill FK_Bill_Payment_BillId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Bill"
    ADD CONSTRAINT "FK_Bill_Payment_BillId" FOREIGN KEY ("BillId") REFERENCES public."Payment"("Id");


--
-- TOC entry 3308 (class 2606 OID 17206)
-- Name: Service FK_Service_AddOnActivation_DataServiceId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Service"
    ADD CONSTRAINT "FK_Service_AddOnActivation_DataServiceId" FOREIGN KEY ("DataServiceId") REFERENCES public."AddOnActivation"("Id");


--
-- TOC entry 3309 (class 2606 OID 17211)
-- Name: Service FK_Service_Bill_ServiceId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Service"
    ADD CONSTRAINT "FK_Service_Bill_ServiceId" FOREIGN KEY ("ServiceId") REFERENCES public."Bill"("Id");


--
-- TOC entry 3310 (class 2606 OID 17216)
-- Name: Service FK_Service_PackageUsage_ServiceId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Service"
    ADD CONSTRAINT "FK_Service_PackageUsage_ServiceId" FOREIGN KEY ("ServiceId") REFERENCES public."PackageUsage"("Id");


--
-- TOC entry 3311 (class 2606 OID 17221)
-- Name: Service FK_Service_Payment_ServiceId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Service"
    ADD CONSTRAINT "FK_Service_Payment_ServiceId" FOREIGN KEY ("ServiceId") REFERENCES public."Payment"("Id");


--
-- TOC entry 3312 (class 2606 OID 17235)
-- Name: User FK_User_AddOnActivation_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "FK_User_AddOnActivation_UserId" FOREIGN KEY ("UserId") REFERENCES public."AddOnActivation"("Id");


--
-- TOC entry 3313 (class 2606 OID 17240)
-- Name: User FK_User_Bill_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "FK_User_Bill_UserId" FOREIGN KEY ("UserId") REFERENCES public."Bill"("Id");


--
-- TOC entry 3314 (class 2606 OID 17245)
-- Name: User FK_User_DataService_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "FK_User_DataService_UserId" FOREIGN KEY ("UserId") REFERENCES public."DataService"("UserId");


--
-- TOC entry 3315 (class 2606 OID 17250)
-- Name: User FK_User_Notification_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "FK_User_Notification_UserId" FOREIGN KEY ("UserId") REFERENCES public."Notification"("Id");


--
-- TOC entry 3316 (class 2606 OID 17255)
-- Name: User FK_User_PackageUsage_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "FK_User_PackageUsage_UserId" FOREIGN KEY ("UserId") REFERENCES public."PackageUsage"("Id");


--
-- TOC entry 3317 (class 2606 OID 17260)
-- Name: User FK_User_Payment_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "FK_User_Payment_UserId" FOREIGN KEY ("UserId") REFERENCES public."Payment"("Id");


--
-- TOC entry 3318 (class 2606 OID 17265)
-- Name: User FK_User_VoiceService_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "FK_User_VoiceService_UserId" FOREIGN KEY ("UserId") REFERENCES public."VoiceService"("UserId");


--
-- TOC entry 3486 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;


-- Completed on 2023-10-05 17:26:19 +0530

--
-- PostgreSQL database dump complete
--

