-- Recomenda��es
    /*
            0. "Recomendamos agendar uma limpeza profissional a cada 6 meses para remover o t�rtaro que n�o pode ser eliminado com escova��o regular e prevenir problemas futuros.",
            1. "Escove os dentes pelo menos duas vezes ao dia durante 2 minutos, utilizando movimentos circulares suaves e prestando aten��o especial � linha da gengiva.",
            2. "Utilize fio dental diariamente para remover a placa bacteriana e res�duos de alimentos entre os dentes, �reas que a escova n�o alcan�a efetivamente.",
            3. "Para reduzir a sensibilidade dent�ria, utilize pasta de dente espec�fica para dentes sens�veis e evite alimentos e bebidas muito quentes, frias ou �cidas.",
            4. "A dor que voc� relatou pode indicar uma infec��o ou c�rie profunda. Recomendamos consulta odontol�gica urgente para avalia��o e tratamento adequado.",
            5. "Seus sintomas sugerem gengivite inicial. Intensifique a higiene bucal, use enxaguante bucal antiss�ptico e agende uma consulta para avalia��o profissional.",
            6. "O desgaste dent�rio observado sugere bruxismo noturno. Recomendamos a confec��o de uma placa de mordida para uso durante o sono.",
            7. "Para aliviar a boca seca, aumente a ingest�o de �gua, evite cafe�na e �lcool, e considere o uso de substitutos de saliva recomendados pelo seu dentista.",
            8. "Reduza o consumo de alimentos e bebidas a�ucaradas ou �cidas, como refrigerantes e sucos c�tricos, para prevenir a eros�o do esmalte e o desenvolvimento de c�ries.",
            9. "Baseado em seus relatos recentes, recomendamos um exame bucal completo, incluindo radiografias, para identificar poss�veis problemas em est�gio inicial."
     */

-- 5 pacientes fict�cios com dados completos
INSERT INTO "T_OPBD_PACIENTE" ("id_paciente", "nm_paciente", "dt_nascimento", "nr_cpf", "ds_sexo", "nr_telefone", "ds_email", "id_plano")
VALUES
(11, 'Ricardo Mendes Silva', TO_DATE('15/03/1975', 'DD/MM/YYYY'), '32145678901', 'M', '11984567123', 'ricardo.mendes@outlook.com', 1);

INSERT INTO "T_OPBD_PACIENTE" ("id_paciente", "nm_paciente", "dt_nascimento", "nr_cpf", "ds_sexo", "nr_telefone", "ds_email", "id_plano")
VALUES
(12, 'Fernanda Alves Costa', TO_DATE('22/09/1992', 'DD/MM/YYYY'), '45612378945', 'F', '11991234567', 'fernanda.alves@gmail.com', 1);

INSERT INTO "T_OPBD_PACIENTE" ("id_paciente", "nm_paciente", "dt_nascimento", "nr_cpf", "ds_sexo", "nr_telefone", "ds_email", "id_plano")
VALUES
(13, 'Carlos Eduardo Santos', TO_DATE('07/11/1980', 'DD/MM/YYYY'), '78932165498', 'M', '11987651234', 'carlosedu.santos@yahoo.com', 1);

INSERT INTO "T_OPBD_PACIENTE" ("id_paciente", "nm_paciente", "dt_nascimento", "nr_cpf", "ds_sexo", "nr_telefone", "ds_email", "id_plano")
VALUES
(14, 'Mariana Oliveira Ferreira', TO_DATE('03/04/1985', 'DD/MM/YYYY'), '12378945612', 'F', '11998765432', 'mari.oliveira@hotmail.com', 1);

INSERT INTO "T_OPBD_PACIENTE" ("id_paciente", "nm_paciente", "dt_nascimento", "nr_cpf", "ds_sexo", "nr_telefone", "ds_email", "id_plano")
VALUES
(15, 'Rodrigo Souza Martins', TO_DATE('18/12/1978', 'DD/MM/YYYY'), '65498731298', 'M', '11976543210', 'rodrigo.souza@gmail.com', 1);

-- Respostas para as perguntas existentes

-- Respostas para dor (recomenda��o 4 - Ricardo)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (101, 'Sim, sinto dor forte e constante no lado direito da boca');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (102, 'A dor � pulsante e n�o melhora com analg�sicos comuns');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (103, 'Sim, a dor aumenta quando mastigo alimentos');

-- Respostas para higiene (recomenda��o 1 - Fernanda)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (104, 'Escovo apenas uma vez por dia, rapidamente de manh�');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (105, 'Nunca uso fio dental, acho inc�modo');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (106, 'Faz mais de um ano que n�o vou ao dentista');

-- Respostas para a��car (recomenda��o 8 - Carlos)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (107, 'Consumo refrigerantes todos os dias, pelo menos tr�s latas');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (108, 'Como doces e sobremesas ap�s todas as refei��es');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (109, 'Bebo suco de lim�o concentrado diariamente');

-- Respostas para bruxismo (recomenda��o 6 - Mariana)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (110, 'Sim, acordo com dor na mand�bula todos os dias');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (111, 'Meu parceiro diz que eu ranjo os dentes a noite inteira');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (112, 'Percebo que meus dentes est�o se desgastando na ponta');

-- Respostas para sensibilidade (recomenda��o 3 - Rodrigo)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (113, 'Sinto muita dor ao tomar bebidas geladas');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (114, 'A sensibilidade � t�o forte que evito sorvetes e �gua fria');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (115, 'N�o uso nenhuma pasta espec�fica para sensibilidade');

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2001, SYSDATE-7, 11, 1, 101);  -- Pergunta sobre dor nas �ltimas semanas
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2002, SYSDATE-6, 11, 1, 102);  -- Repeti��o da pergunta sobre dor (resposta diferente)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2003, SYSDATE-5, 11, 1, 103);  -- Novamente sobre dor (terceira resposta)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2004, SYSDATE-4, 11, 5, 113);  -- Pergunta sobre sensibilidade
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2005, SYSDATE-3, 11, 10, 110); -- Pergunta sobre dores na mand�bula
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2006, SYSDATE-2, 11, 12, 101); -- Pergunta sobre incha�o (resposta sobre dor)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2007, SYSDATE-1, 11, 1, 101);  -- Repeti��o final da pergunta sobre dor

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2008, SYSDATE-7, 12, 3, 104);  -- Pergunta sobre frequ�ncia de escova��o
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2009, SYSDATE-6, 12, 4, 105);  -- Pergunta sobre uso de fio dental
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2010, SYSDATE-5, 12, 8, 106);  -- Pergunta sobre �ltima consulta
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2011, SYSDATE-4, 12, 15, 104); -- Pergunta sobre produtos de higiene
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2012, SYSDATE-3, 12, 3, 104); -- Repeti��o da pergunta sobre escova��o
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2013, SYSDATE-2, 12, 4, 105);  -- Repeti��o da pergunta sobre fio dental
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2014, SYSDATE-1, 12, 3, 104);  -- Repeti��o final sobre escova��o

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2015, SYSDATE-7, 13, 9, 107);  -- Pergunta sobre consumo de bebidas a�ucaradas
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2016, SYSDATE-6, 13, 9, 108);  -- Repeti��o sobre consumo de a��car (resposta diferente)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2017, SYSDATE-5, 13, 9, 109);  -- Novamente sobre alimentos �cidos
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2018, SYSDATE-4, 13, 9, 107);  -- Repeti��o sobre refrigerantes
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2019, SYSDATE-3, 13, 9, 108);  -- Repeti��o sobre doces
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2020, SYSDATE-2, 13, 11, 108); -- Pergunta sobre manchas nos dentes
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2021, SYSDATE-1, 13, 9, 107);  -- Repeti��o final sobre a��car

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2022, SYSDATE-7, 14, 10, 110); -- Pergunta sobre dores na mand�bula
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2023, SYSDATE-6, 14, 10, 111); -- Repeti��o sobre ranger os dentes
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2024, SYSDATE-5, 14, 10, 112); -- Novamente sobre desgaste
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2025, SYSDATE-4, 14, 10, 110); -- Repeti��o sobre dores na mand�bula
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2026, SYSDATE-3, 14, 10, 111); -- Repeti��o sobre ranger os dentes
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2027, SYSDATE-2, 14, 10, 112); -- Repeti��o sobre desgaste
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2028, SYSDATE-1, 14, 10, 110); -- Repeti��o final sobre bruxismo

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2029, SYSDATE-7, 15, 5, 113); -- Pergunta sobre sensibilidade
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2030, SYSDATE-6, 15, 5, 114);  -- Repeti��o sobre sensibilidade (resposta diferente)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2031, SYSDATE-5, 15, 15, 115); -- Pergunta sobre produtos (n�o usa pasta para sensibilidade)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2032, SYSDATE-4, 15, 5, 113);  -- Repeti��o sobre sensibilidade
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2033, SYSDATE-3, 15, 5, 114);  -- Repeti��o sobre evitar alimentos
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2034, SYSDATE-2, 15, 5, 113);  -- Repeti��o sobre sensibilidade
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2035, SYSDATE-1, 15, 5, 114);  -- Repeti��o final sobre sensibilidade

