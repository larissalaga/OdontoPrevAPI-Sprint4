-- Limpar dados antigos (opcional - use apenas se necess�rio)
 DELETE FROM "T_OPBD_CHECK_IN" WHERE "id_check_in" > 0;
 DELETE FROM "T_OPBD_RESPOSTAS" WHERE "id_resposta" > 0;
 DELETE FROM "T_OPBD_PERGUNTAS" WHERE "id_pergunta" > 0;

-- Inserir perguntas odontol�gicas relevantes
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (1, 'Voc� sentiu alguma dor nos dentes ou gengivas nas �ltimas duas semanas?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (2, 'Suas gengivas sangram durante a escova��o ou uso de fio dental?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (3, 'Com que frequ�ncia e como voc� escova seus dentes diariamente?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (4, 'Voc� utiliza fio dental regularmente? Com que frequ�ncia?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (5, 'Voc� sente sensibilidade ao consumir alimentos ou bebidas quentes, frias ou doces?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (6, 'Com que frequ�ncia voc� sente a boca seca ao longo do dia?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (7, 'Voc� percebe mau h�lito mesmo ap�s a escova��o?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (8, 'Quando foi sua �ltima consulta com dentista e qual foi o procedimento realizado?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (9, 'Com que frequ�ncia voc� consome bebidas a�ucaradas ou alimentos �cidos no seu dia a dia?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (10, 'Voc� acorda com dores na mand�bula ou percebe que range os dentes durante o sono?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (11, 'Voc� notou alguma mancha ou mudan�a na colora��o dos seus dentes recentemente?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (12, 'Voc� tem percebido incha�o ou caro�os na boca, l�ngua ou nas gengivas?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (13, 'Algum de seus dentes parece estar solto ou com maior mobilidade do que o normal?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (14, 'Voc� usa algum medicamento que causa boca seca como efeito colateral?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (15, 'Qual tipo de escova, pasta de dente e produtos de higiene bucal voc� utiliza?');

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

-- Inserir respostas com conte�do textual rico para an�lise
-- Respostas relacionadas a dor (para recomenda��o 4)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (1, 'Sim, tenho sentido uma dor forte e constante nos �ltimos dias, principalmente no lado direito da boca');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (2, 'Sim, sinto dor intensa quando mastigo alimentos ou bebo �gua gelada');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (3, 'Estou com uma dor pulsante que come�ou h� tr�s dias e est� piorando');

-- Respostas relacionadas a sangramento gengival (para recomenda��o 5)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (4, 'Sim, minhas gengivas sangram quase todas as vezes que escovo os dentes');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (5, '�s vezes noto sangue na escova e quando uso fio dental');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (6, 'Recentemente comecei a notar sangramento nas gengivas, principalmente pela manh�');

-- Respostas sobre h�bitos de escova��o (para recomenda��o 1)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (7, 'Escovo apenas uma vez por dia rapidamente antes de dormir');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (8, 'Duas vezes ao dia, mas acho que n�o estou fazendo corretamente pois sinto que ficam res�duos');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (9, 'Tr�s vezes ao dia, ap�s as refei��es, por cerca de um minuto cada vez');

-- Respostas sobre uso de fio dental (para recomenda��o 2)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (10, 'Raramente uso fio dental, apenas quando sinto algo preso entre os dentes');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (11, 'Nunca adquiri o h�bito de usar fio dental, acho desconfort�vel');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (12, 'Uso fio dental apenas nos finais de semana quando tenho mais tempo');

-- Respostas sobre sensibilidade (para recomenda��o 3)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (13, 'Sim, sinto muita sensibilidade ao tomar bebidas geladas ou sorvete');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (14, 'Tenho forte sensibilidade no lado esquerdo ao consumir alimentos �cidos ou gelados');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (15, 'Recentemente desenvolvi sensibilidade em v�rios dentes, especialmente com comidas doces');

-- Respostas sobre boca seca (para recomenda��o 7)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (16, 'Frequentemente sinto a boca seca, principalmente � noite e ao acordar');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (17, 'Sim, tenho boca seca constante e preciso beber �gua v�rias vezes durante o dia');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (18, 'Desde que comecei a tomar um novo medicamento, sinto minha boca constantemente seca');

-- Respostas sobre bruxismo (para recomenda��o 6)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (19, 'Sim, acordo com dor na mand�bula e meu parceiro diz que ranjo os dentes � noite');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (20, 'Tenho notado desgaste nos meus dentes e frequentemente acordo com dores de cabe�a');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (21, 'Minha mand�bula estala quando abro a boca e sinto tens�o muscular ao acordar');

-- Respostas sobre consumo de a��car (para recomenda��o 8)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (22, 'Consumo refrigerantes e sucos diariamente, v�rias vezes ao dia');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (23, 'Como muitos doces e bebo caf� com a��car v�rias vezes ao dia');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (24, 'Tenho o h�bito de tomar bebidas c�tricas frequentemente como limonada e suco de laranja');

-- Respostas sobre �ltima consulta (para recomenda��o 0 e 9)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (25, 'Minha �ltima consulta foi h� mais de 2 anos, n�o lembro o procedimento');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (26, 'Nunca fui ao dentista desde que me mudei para c�, h� aproximadamente um ano');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (27, 'Faz uns 7 meses que fiz uma limpeza de rotina');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (28, 'Fa�o limpeza a cada 6 meses, a �ltima foi h� cerca de 5 meses');

-- Respostas neutras ou positivas
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (29, 'N�o, n�o tenho sentido nenhuma dor ou desconforto');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (30, 'Minhas gengivas n�o sangram durante a escova��o');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (31, 'Uso fio dental todos os dias, uma vez ao dia antes de dormir');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (32, 'N�o sinto sensibilidade com alimentos quentes ou frios');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (33, 'N�o tenho problema com boca seca');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (34, 'N�o consumo refrigerantes ou doces com frequ�ncia');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (35, 'N�o ranjo os dentes durante o sono');

-- PACIENTE 1: Perfil com problemas de DOR (recomenda��o 4)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (101, CURRENT_DATE-4, 1, 1, 1); -- Dor forte e constante

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (102, CURRENT_DATE-4, 1, 5, 14); -- Tamb�m tem sensibilidade

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (103, CURRENT_DATE-4, 1, 8, 25); -- N�o vai ao dentista h� mais de 2 anos

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (104, CURRENT_DATE-3, 1, 1, 2); -- Confirma dor ao mastigar

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (105, CURRENT_DATE-3, 1, 12, 29); -- N�o tem incha�o

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (106, CURRENT_DATE-0, 1, 1, 3); -- Dor pulsante que est� piorando

-- PACIENTE 2: Perfil com problemas de GENGIVITE (recomenda��o 5)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (201, CURRENT_DATE-4, 2, 2, 4); -- Gengivas sangram ao escovar

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (202, CURRENT_DATE-4, 2, 3, 8); -- Escova��o inadequada

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (203, CURRENT_DATE-4, 2, 4, 10); -- Uso irregular de fio dental

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (204, CURRENT_DATE-2, 2, 2, 6); -- Sangramento recente nas gengivas

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (205, CURRENT_DATE-2, 2, 7, 29); -- N�o tem mau h�lito

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (206, CURRENT_DATE-0, 2, 2, 5); -- Confirma o sangramento

-- PACIENTE 3: Perfil com problemas de HIGIENE - ESCOVA��O (recomenda��o 1)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (301, CURRENT_DATE-4, 3, 3, 7); -- Escova apenas uma vez por dia rapidamente

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (302, CURRENT_DATE-4, 3, 7, 29); -- N�o tem problema de mau h�lito

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (303, CURRENT_DATE-4, 3, 15, 28); -- Faz limpeza regularmente

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (304, CURRENT_DATE-2, 3, 3, 8); -- Confirma escova��o inadequada

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (305, CURRENT_DATE-0, 3, 11, 29); -- N�o tem manchas nos dentes

-- PACIENTE 4: Perfil com problemas de FIO DENTAL (recomenda��o 2)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (401, CURRENT_DATE-4, 4, 4, 11); -- Nunca usa fio dental

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (402, CURRENT_DATE-4, 4, 3, 9); -- Escova tr�s vezes ao dia (bom)

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (403, CURRENT_DATE-4, 4, 2, 5); -- Tem algum sangramento

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (404, CURRENT_DATE-2, 4, 4, 10); -- Confirma uso irregular de fio dental

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (405, CURRENT_DATE-0, 4, 8, 27); -- Fez limpeza h� 7 meses

-- PACIENTE 5: Perfil com SENSIBILIDADE (recomenda��o 3)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (501, CURRENT_DATE-4, 5, 5, 13); -- Forte sensibilidade

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (502, CURRENT_DATE-4, 5, 9, 24); -- Consome bebidas �cidas

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (503, CURRENT_DATE-4, 5, 3, 9); -- Escova��o adequada

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (504, CURRENT_DATE-2, 5, 5, 15); -- Confirma sensibilidade com doces

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (505, CURRENT_DATE-0, 5, 1, 29); -- N�o tem dor

-- PACIENTE 6: Perfil com BRUXISMO (recomenda��o 6)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (601, CURRENT_DATE-4, 6, 10, 19); -- Range os dentes

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (602, CURRENT_DATE-4, 6, 1, 29); -- N�o tem dor

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (603, CURRENT_DATE-4, 6, 8, 27); -- Consulta recente

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (604, CURRENT_DATE-2, 6, 10, 20); -- Confirma desgaste nos dentes

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (605, CURRENT_DATE-0, 6, 10, 21); -- Mand�bula estala

-- PACIENTE 7: Perfil com BOCA SECA (recomenda��o 7)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (701, CURRENT_DATE-4, 7, 6, 16); -- Boca seca frequente

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (702, CURRENT_DATE-4, 7, 14, 18); -- Usa medicamento que causa boca seca

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (703, CURRENT_DATE-4, 7, 3, 9); -- Escova��o adequada

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (704, CURRENT_DATE-2, 7, 6, 17); -- Confirma boca seca constante

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (705, CURRENT_DATE-0, 7, 7, 29); -- N�o tem mau h�lito

-- PACIENTE 8: Perfil com consumo excessivo de A��CAR (recomenda��o 8)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (801, CURRENT_DATE-4, 8, 9, 22); -- Consome refrigerantes diariamente

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (802, CURRENT_DATE-4, 8, 11, 29); -- N�o tem manchas nos dentes

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (803, CURRENT_DATE-4, 8, 3, 9); -- Escova bem

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (804, CURRENT_DATE-2, 8, 9, 23); -- Confirma consumo de doces

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (805, CURRENT_DATE-0, 8, 5, 15); -- Sensibilidade com doces

-- PACIENTE 9: Perfil para LIMPEZA PROFISSIONAL (recomenda��o 0)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (901, CURRENT_DATE-4, 9, 8, 27); -- �ltima limpeza h� 7 meses

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (902, CURRENT_DATE-4, 9, 3, 9); -- Escova bem

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (903, CURRENT_DATE-4, 9, 4, 31); -- Usa fio dental

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (904, CURRENT_DATE-2, 9, 7, 29); -- Sem mau h�lito

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (905, CURRENT_DATE-0, 9, 1, 29); -- Sem dor

-- PACIENTE 10: Perfil para EXAME COMPLETO (recomenda��o 9)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (1001, CURRENT_DATE-4, 10, 8, 25); -- N�o vai h� mais de 2 anos

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (1002, CURRENT_DATE-4, 10, 13, 29); -- Sem dentes soltos

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (1003, CURRENT_DATE-4, 10, 3, 8); -- Escova��o inadequada

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (1004, CURRENT_DATE-2, 10, 8, 26); -- Confirma que nunca foi ao dentista na cidade atual

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (1005, CURRENT_DATE-0, 10, 12, 29); -- Sem caro�os na boca

-- COMMIT
COMMIT;