-- Recomendações
    /*
            0. "Recomendamos agendar uma limpeza profissional a cada 6 meses para remover o tártaro que não pode ser eliminado com escovação regular e prevenir problemas futuros.",
            1. "Escove os dentes pelo menos duas vezes ao dia durante 2 minutos, utilizando movimentos circulares suaves e prestando atenção especial à linha da gengiva.",
            2. "Utilize fio dental diariamente para remover a placa bacteriana e resíduos de alimentos entre os dentes, áreas que a escova não alcança efetivamente.",
            3. "Para reduzir a sensibilidade dentária, utilize pasta de dente específica para dentes sensíveis e evite alimentos e bebidas muito quentes, frias ou ácidas.",
            4. "A dor que você relatou pode indicar uma infecção ou cárie profunda. Recomendamos consulta odontológica urgente para avaliação e tratamento adequado.",
            5. "Seus sintomas sugerem gengivite inicial. Intensifique a higiene bucal, use enxaguante bucal antisséptico e agende uma consulta para avaliação profissional.",
            6. "O desgaste dentário observado sugere bruxismo noturno. Recomendamos a confecção de uma placa de mordida para uso durante o sono.",
            7. "Para aliviar a boca seca, aumente a ingestão de água, evite cafeína e álcool, e considere o uso de substitutos de saliva recomendados pelo seu dentista.",
            8. "Reduza o consumo de alimentos e bebidas açucaradas ou ácidas, como refrigerantes e sucos cítricos, para prevenir a erosão do esmalte e o desenvolvimento de cáries.",
            9. "Baseado em seus relatos recentes, recomendamos um exame bucal completo, incluindo radiografias, para identificar possíveis problemas em estágio inicial."
     */

-- 5 pacientes fictícios com dados completos
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

-- Respostas para dor (recomendação 4 - Ricardo)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (101, 'Sim, sinto dor forte e constante no lado direito da boca');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (102, 'A dor é pulsante e não melhora com analgésicos comuns');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (103, 'Sim, a dor aumenta quando mastigo alimentos');

-- Respostas para higiene (recomendação 1 - Fernanda)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (104, 'Escovo apenas uma vez por dia, rapidamente de manhã');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (105, 'Nunca uso fio dental, acho incômodo');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (106, 'Faz mais de um ano que não vou ao dentista');

-- Respostas para açúcar (recomendação 8 - Carlos)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (107, 'Consumo refrigerantes todos os dias, pelo menos três latas');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (108, 'Como doces e sobremesas após todas as refeições');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (109, 'Bebo suco de limão concentrado diariamente');

-- Respostas para bruxismo (recomendação 6 - Mariana)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (110, 'Sim, acordo com dor na mandíbula todos os dias');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (111, 'Meu parceiro diz que eu ranjo os dentes a noite inteira');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (112, 'Percebo que meus dentes estão se desgastando na ponta');

-- Respostas para sensibilidade (recomendação 3 - Rodrigo)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (113, 'Sinto muita dor ao tomar bebidas geladas');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (114, 'A sensibilidade é tão forte que evito sorvetes e água fria');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (115, 'Não uso nenhuma pasta específica para sensibilidade');

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2001, SYSDATE-7, 11, 1, 101);  -- Pergunta sobre dor nas últimas semanas
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2002, SYSDATE-6, 11, 1, 102);  -- Repetição da pergunta sobre dor (resposta diferente)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2003, SYSDATE-5, 11, 1, 103);  -- Novamente sobre dor (terceira resposta)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2004, SYSDATE-4, 11, 5, 113);  -- Pergunta sobre sensibilidade
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2005, SYSDATE-3, 11, 10, 110); -- Pergunta sobre dores na mandíbula
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2006, SYSDATE-2, 11, 12, 101); -- Pergunta sobre inchaço (resposta sobre dor)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2007, SYSDATE-1, 11, 1, 101);  -- Repetição final da pergunta sobre dor

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2008, SYSDATE-7, 12, 3, 104);  -- Pergunta sobre frequência de escovação
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2009, SYSDATE-6, 12, 4, 105);  -- Pergunta sobre uso de fio dental
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2010, SYSDATE-5, 12, 8, 106);  -- Pergunta sobre última consulta
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2011, SYSDATE-4, 12, 15, 104); -- Pergunta sobre produtos de higiene
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2012, SYSDATE-3, 12, 3, 104); -- Repetição da pergunta sobre escovação
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2013, SYSDATE-2, 12, 4, 105);  -- Repetição da pergunta sobre fio dental
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2014, SYSDATE-1, 12, 3, 104);  -- Repetição final sobre escovação

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2015, SYSDATE-7, 13, 9, 107);  -- Pergunta sobre consumo de bebidas açucaradas
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2016, SYSDATE-6, 13, 9, 108);  -- Repetição sobre consumo de açúcar (resposta diferente)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2017, SYSDATE-5, 13, 9, 109);  -- Novamente sobre alimentos ácidos
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2018, SYSDATE-4, 13, 9, 107);  -- Repetição sobre refrigerantes
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2019, SYSDATE-3, 13, 9, 108);  -- Repetição sobre doces
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2020, SYSDATE-2, 13, 11, 108); -- Pergunta sobre manchas nos dentes
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2021, SYSDATE-1, 13, 9, 107);  -- Repetição final sobre açúcar

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2022, SYSDATE-7, 14, 10, 110); -- Pergunta sobre dores na mandíbula
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2023, SYSDATE-6, 14, 10, 111); -- Repetição sobre ranger os dentes
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2024, SYSDATE-5, 14, 10, 112); -- Novamente sobre desgaste
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2025, SYSDATE-4, 14, 10, 110); -- Repetição sobre dores na mandíbula
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2026, SYSDATE-3, 14, 10, 111); -- Repetição sobre ranger os dentes
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2027, SYSDATE-2, 14, 10, 112); -- Repetição sobre desgaste
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2028, SYSDATE-1, 14, 10, 110); -- Repetição final sobre bruxismo

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2029, SYSDATE-7, 15, 5, 113); -- Pergunta sobre sensibilidade
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2030, SYSDATE-6, 15, 5, 114);  -- Repetição sobre sensibilidade (resposta diferente)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2031, SYSDATE-5, 15, 15, 115); -- Pergunta sobre produtos (não usa pasta para sensibilidade)
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2032, SYSDATE-4, 15, 5, 113);  -- Repetição sobre sensibilidade
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2033, SYSDATE-3, 15, 5, 114);  -- Repetição sobre evitar alimentos
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2034, SYSDATE-2, 15, 5, 113);  -- Repetição sobre sensibilidade
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta") VALUES (2035, SYSDATE-1, 15, 5, 114);  -- Repetição final sobre sensibilidade

