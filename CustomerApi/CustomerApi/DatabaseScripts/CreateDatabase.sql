--
-- PostgreSQL database dump
--

-- Dumped from database version 11.3
-- Dumped by pg_dump version 11.3

-- Started on 2021-07-13 12:55:27

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

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 199 (class 1259 OID 41021)
-- Name: address; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.address (
    id integer NOT NULL,
    line1 character varying(80) NOT NULL,
    line2 character varying(80),
    town character varying(50) NOT NULL,
    county character varying(50),
    postcode character varying(10) NOT NULL,
    country character varying(80),
    main boolean NOT NULL,
    customer_id integer NOT NULL
);


ALTER TABLE public.address OWNER TO postgres;

--
-- TOC entry 198 (class 1259 OID 41019)
-- Name: address_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.address ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.address_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 197 (class 1259 OID 41014)
-- Name: customer; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.customer (
    id integer NOT NULL,
    active boolean NOT NULL,
    title character varying(20) NOT NULL,
    forename character varying(50) NOT NULL,
    surname character varying(50) NOT NULL,
    email character varying(75) NOT NULL,
    mobile character varying(15) NOT NULL
);


ALTER TABLE public.customer OWNER TO postgres;

--
-- TOC entry 196 (class 1259 OID 41012)
-- Name: customer_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.customer ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.customer_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 2696 (class 2606 OID 41085)
-- Name: address address_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.address
    ADD CONSTRAINT address_pkey PRIMARY KEY (id);


--
-- TOC entry 2692 (class 2606 OID 41018)
-- Name: customer customer_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customer
    ADD CONSTRAINT customer_pkey PRIMARY KEY (id);


--
-- TOC entry 2694 (class 2606 OID 41109)
-- Name: customer unq_customer; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customer
    ADD CONSTRAINT unq_customer UNIQUE (title, forename, surname, email, mobile);


--
-- TOC entry 2697 (class 2606 OID 41103)
-- Name: address fk_address_customer_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.address
    ADD CONSTRAINT fk_address_customer_id FOREIGN KEY (customer_id) REFERENCES public.customer(id);


-- Completed on 2021-07-13 12:55:28

--
-- PostgreSQL database dump complete
--

