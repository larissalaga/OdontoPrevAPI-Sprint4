-- DELETE PACIENTE
BEGIN
    "DELETE_PACIENTE"
('18207586322');
END;
--
CREATE
OR REPLACE PROCEDURE "DELETE_PACIENTE"(
    "p_nr_cpf" VARCHAR2
) IS
    "paciente_id" NUMBER;
    paciente_not_found
EXCEPTION;
    -- Preparação para deletar as tabelas de check-in e respostas
    TYPE
lista_t IS TABLE OF "T_OPBD_RESPOSTAS"."id_resposta"%TYPE;
    lista_respostas
lista_t;

BEGIN
    -- Busca o id do paciente por cpf
BEGIN
SELECT "id_paciente"
INTO "paciente_id"
FROM "T_OPBD_PACIENTE"
WHERE "nr_cpf" = "p_nr_cpf";
EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RAISE paciente_not_found;
END;
    
    -- Deleta da tabela Check_in. Foi feito dessa forma pois se o check-in é deletado a tabela de respostas
-- perde a referência. E a tabela de respostas não pode ser deletada antes pois a check-in usa uma fk dela
DELETE
FROM "T_OPBD_CHECK_IN"
WHERE "id_paciente" = "paciente_id" RETURNING "id_resposta"
BULK COLLECT
INTO lista_respostas;
DBMS_OUTPUT
.
PUT_LINE
('Check_in deletado com sucesso.');

FOR i IN lista_respostas.FIRST .. lista_respostas.LAST LOOP
DELETE
FROM "T_OPBD_RESPOSTAS"
WHERE "id_resposta" = lista_respostas(i);
END LOOP;
DBMS_OUTPUT.PUT_LINE
('Respostas deletadas com sucesso.');

-- Deleta da tabela Analise_Raio_x
DELETE
FROM "T_OPBD_ANALISE_RAIO_X"
WHERE "id_raio_x" IN (SELECT "id_raio_x"
                      FROM "T_OPBD_RAIO_X"
                      WHERE "id_paciente" = "paciente_id");
DBMS_OUTPUT
.
PUT_LINE
('Análise deletada com sucesso.');

-- Deleta da tabela Raio_x
DELETE
FROM "T_OPBD_RAIO_X"
WHERE "id_paciente" = "paciente_id";
DBMS_OUTPUT
.
PUT_LINE
('Raio_x deletado com sucesso.');

        
-- Deleta da tabela Paciente_Dentista
DELETE
FROM "T_OPBD_PACIENTE_DENTISTA"
WHERE "id_paciente" = "paciente_id";
DBMS_OUTPUT
.
PUT_LINE
('Relacionamentos Paciente_Dentista deletados com sucesso.');

-- Deleta da tabela Extrato_Pontos
DELETE
FROM "T_OPBD_EXTRATO_PONTOS"
WHERE "id_paciente" = "paciente_id";
DBMS_OUTPUT
.
PUT_LINE
('Extratos de Pontos deletados com sucesso.');

-- Deleta da tabela Paciente
DELETE
FROM "T_OPBD_PACIENTE"
WHERE "nr_cpf" = "p_nr_cpf";
DBMS_OUTPUT
.
PUT_LINE
('Paciente deletado com sucesso.');

COMMIT;

EXCEPTION
    WHEN paciente_not_found THEN
        RAISE_APPLICATION_ERROR(-20001, 'Paciente não existe.');
WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR (-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END "DELETE_PACIENTE";
/
--------------------------------------------------------------------------------------------------------------------------------
-- DELETE DENTISTA
BEGIN
    "DELETE_DENTISTA"
('');
END;
--

CREATE
OR REPLACE PROCEDURE "DELETE_DENTISTA"(
    "p_ds_cro" VARCHAR2
) IS
BEGIN

    -- Deleta os relacionamentos com o dentista
DELETE
FROM "T_OPBD_PACIENTE_DENTISTA"
WHERE "id_dentista" IN (SELECT "id_dentista"
                        FROM "T_OPBD_DENTISTA"
                        WHERE "ds_cro" = "p_ds_cro");
DBMS_OUTPUT
.
PUT_LINE
('Relacionamento Paciente_Dentista deletado com sucesso.');
    -- Deleta da tabela Dentista
DELETE
FROM "T_OPBD_DENTISTA"
WHERE "ds_cro" = "p_ds_cro";
DBMS_OUTPUT
.
PUT_LINE
('Dentista deletado com sucesso.');

COMMIT;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20001, 'Documento não encontrado.');
WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR (-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END "DELETE_DENTISTA";
/
--------------------------------------------------------------------------------------------------------------------------------
-- DELETE PLANO
BEGIN
    "DELETE_PLANO"
('');
END;
--
CREATE
OR REPLACE PROCEDURE "DELETE_PLANO"(
    "p_ds_codigo_plano" VARCHAR2
) IS
    "plano_id" NUMBER;
BEGIN
    -- Não vamos deletar um plano se ele já estiver sendo utilizado por um paciente
DELETE
FROM "T_OPBD_PLANO"
WHERE "ds_codigo_plano" = "p_ds_codigo_plano"
  AND "id_plano" NOT IN (SELECT "id_plano"
                         FROM "T_OPBD_PACIENTE");
DBMS_OUTPUT
.
PUT_LINE
('Plano deletado com sucesso.');

COMMIT;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20001, 'Plano não encontrado.');
WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR (-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END "DELETE_PLANO";
/
--------------------------------------------------------------------------------------------------------------------------------
-- DELETE PERGUNTAS
BEGIN
    "DELETE_PERGUNTAS"
('');
END;
--
CREATE
OR REPLACE PROCEDURE "DELETE_PERGUNTAS"(
    "p_id_pergunta" NUMBER
) IS
BEGIN
    -- Não vamos deletar uma pergunta se já tiver uma check_in que a utilizou anteriormente
DELETE
FROM "T_OPBD_PERGUNTAS"
WHERE "id_pergunta" = "p_id_pergunta"
  AND "id_pergunta" NOT IN (SELECT "id_pergunta"
                            FROM "T_OPBD_CHECK_IN");
DBMS_OUTPUT
.
PUT_LINE
('Pergunta deletada com sucesso.');

COMMIT;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20001, 'Pergunta não encontrada.');
WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR (-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END "DELETE_PERGUNTAS";
/
--------------------------------------------------------------------------------------------------------------------------------
-- DELETE RESPOSTAS
BEGIN
    "DELETE_RESPOSTAS"
('');
END;
--
CREATE
OR REPLACE PROCEDURE "DELETE_RESPOSTAS"(
    "p_id_resposta" NUMBER
) IS
BEGIN
    -- Deleta da tabela Check_in
DELETE
FROM "T_OPBD_CHECK_IN"
WHERE "id_resposta" = "p_id_resposta";
DBMS_OUTPUT
.
PUT_LINE
('Resposta deletada com sucesso.');

    -- Deleta da tabela Respostas
DELETE
FROM "T_OPBD_RESPOSTAS"
WHERE "id_resposta" = "p_id_resposta";

COMMIT;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20001, 'Resposta não encontrada.');
WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR (-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END "DELETE_RESPOSTAS";
/
--------------------------------------------------------------------------------------------------------------------------------
-- DELETE EXTRATO_PONTOS
BEGIN
    "DELETE_EXTRATO_PONTOS"
('');
END;
--
CREATE
OR REPLACE PROCEDURE "DELETE_EXTRATO_PONTOS"(
    "p_id_extrato_pontos" NUMBER
) IS
BEGIN
    -- Deleta da tabela Extrato_pontos
DELETE
FROM "T_OPBD_EXTRATO_PONTOS"
WHERE "id_extrato_pontos" = "p_id_extrato_pontos";
DBMS_OUTPUT
.
PUT_LINE
('Extrato deletado com sucesso.');

COMMIT;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20001, 'Extrato não encontrado.');
WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR (-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END "DELETE_EXTRATO_PONTOS";
/
--------------------------------------------------------------------------------------------------------------------------------
-- DELETE CHECK_IN
BEGIN
    "DELETE_CHECK_IN"
('');
END;
--
CREATE
OR REPLACE PROCEDURE "DELETE_CHECK_IN"(
    "p_id_check_in" NUMBER
) IS
    "resposta_id" NUMBER;
BEGIN
SELECT "id_resposta"
INTO "resposta_id"
FROM "T_OPBD_CHECK_IN"
WHERE "id_check_in" = "p_id_check_in";

-- Deleta da tabela Check_in
DELETE
FROM "T_OPBD_CHECK_IN"
WHERE "id_check_in" = "p_id_check_in";
DBMS_OUTPUT
.
PUT_LINE
('Check_in deletado com sucesso.');

    -- Deleta da tabela Respostas
DELETE
FROM "T_OPBD_RESPOSTAS"
WHERE "id_resposta" = "resposta_id";
DBMS_OUTPUT
.
PUT_LINE
('Resposta deletada com sucesso.');

COMMIT;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20001, 'Check_in não encontrado.');
WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR (-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END "DELETE_CHECK_IN";
/
--------------------------------------------------------------------------------------------------------------------------------
-- DELETE RAIO_X
BEGIN
    "DELETE_RAIO_X"
('');
END;
--
CREATE
OR REPLACE PROCEDURE "DELETE_RAIO_X"(
    "p_id_raio_x" NUMBER
) IS
BEGIN
    -- Deleta da tabela Analise_Raio_x
DELETE
FROM "T_OPBD_ANALISE_RAIO_X"
WHERE "id_raio_x" = "p_id_raio_x";
DBMS_OUTPUT
.
PUT_LINE
('Análise deletada com sucesso.');

    -- Deleta da tabela Raio_x
DELETE
FROM "T_OPBD_RAIO_X"
WHERE "id_raio_x" = "p_id_raio_x";
DBMS_OUTPUT
.
PUT_LINE
('Raio_x deletado com sucesso.');

COMMIT;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20001, 'Raio_x não encontrado.');
WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR (-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END "DELETE_RAIO_X";
/
--------------------------------------------------------------------------------------------------------------------------------
-- DELETE ANALISE_RAIO_X
BEGIN
    "DELETE_ANALISE_RAIO_X"
('');
END;
--
CREATE
OR REPLACE PROCEDURE "DELETE_ANALISE_RAIO_X"(
    "p_id_analise_raio_x" NUMBER
) IS
BEGIN

DELETE
FROM "T_OPBD_ANALISE_RAIO_X"
WHERE "id_analise_raio_x" = "p_id_analise_raio_x";
DBMS_OUTPUT
.
PUT_LINE
('Análise do raio_x deletada com sucesso.');

COMMIT;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20001, 'Análise do raio_x não encontrada.');
WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR (-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END "DELETE_ANALISE_RAIO_X";
/
--------------------------------------------------------------------------------------------------------------------------------
-- DELETE PACIENTE_DENTISTA
BEGIN
    "DELETE_PACIENTE_DENTISTA"
('');
END;
--
CREATE
OR REPLACE PROCEDURE "DELETE_PACIENTE_DENTISTA"(
    "p_ds_cro" VARCHAR2,
    "p_nr_cpf" VARCHAR2
) IS
    "dentista_id" NUMBER;
    "paciente_id"
NUMBER;
    dentista_not_found
EXCEPTION;
    paciente_not_found
EXCEPTION;
BEGIN
    -- Busca o id do paciente por cpf
BEGIN
SELECT "id_paciente"
INTO "paciente_id"
FROM "T_OPBD_PACIENTE"
WHERE "nr_cpf" = "p_nr_cpf";
EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RAISE paciente_not_found;
END;

    -- Busca o id do Dentista por cro
BEGIN
SELECT "id_dentista"
INTO "dentista_id"
FROM "T_OPBD_DENTISTA"
WHERE "ds_cro" = "p_ds_cro";
EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RAISE dentista_not_found;
END;

    -- Deleta da tabela Paciente_Dentista
DELETE
FROM "T_OPBD_PACIENTE_DENTISTA"
WHERE ("dentista_id" = "id_dentista")
  AND ("paciente_id" = "id_paciente");
DBMS_OUTPUT
.
PUT_LINE
('Relacionamento paciente-dentista deletado com sucesso.');

COMMIT;
EXCEPTION
    WHEN paciente_not_found THEN
        RAISE_APPLICATION_ERROR(-20001, 'Paciente n�o existe.');
WHEN dentista_not_found THEN
        RAISE_APPLICATION_ERROR(-20002, 'Dentista n�o existe.');
WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20003, 'Relacionamento n�o encontrado.');
WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR (-20000, 'ERRO DESCONHECIDO.' || SQLERRM);
END "DELETE_PACIENTE_DENTISTA";
/