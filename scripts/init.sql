
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Tabla de productos
CREATE TABLE public.products (
    id         UUID         NOT NULL DEFAULT uuid_generate_v4(),
    name       VARCHAR(100) NOT NULL,
    price      NUMERIC(18,2)NOT NULL,
    is_active  BOOLEAN      NOT NULL DEFAULT TRUE,
    CONSTRAINT pk_products PRIMARY KEY (id)
);
