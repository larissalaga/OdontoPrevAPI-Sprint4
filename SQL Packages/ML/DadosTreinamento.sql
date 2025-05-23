-- Limpar dados antigos (opcional - use apenas se necessário)
 DELETE FROM "T_OPBD_CHECK_IN" WHERE "id_check_in" > 0;
 DELETE FROM "T_OPBD_RESPOSTAS" WHERE "id_resposta" > 0;
 DELETE FROM "T_OPBD_PERGUNTAS" WHERE "id_pergunta" > 0;

-- Inserir perguntas odontológicas relevantes
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (1, 'Você sentiu alguma dor nos dentes ou gengivas nas últimas duas semanas?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (2, 'Suas gengivas sangram durante a escovação ou uso de fio dental?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (3, 'Com que frequência e como você escova seus dentes diariamente?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (4, 'Você utiliza fio dental regularmente? Com que frequência?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (5, 'Você sente sensibilidade ao consumir alimentos ou bebidas quentes, frias ou doces?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (6, 'Com que frequência você sente a boca seca ao longo do dia?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (7, 'Você percebe mau hálito mesmo após a escovação?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (8, 'Quando foi sua última consulta com dentista e qual foi o procedimento realizado?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (9, 'Com que frequência você consome bebidas açucaradas ou alimentos ácidos no seu dia a dia?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (10, 'Você acorda com dores na mandíbula ou percebe que range os dentes durante o sono?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (11, 'Você notou alguma mancha ou mudança na coloração dos seus dentes recentemente?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (12, 'Você tem percebido inchaço ou caroços na boca, língua ou nas gengivas?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (13, 'Algum de seus dentes parece estar solto ou com maior mobilidade do que o normal?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (14, 'Você usa algum medicamento que causa boca seca como efeito colateral?');
INSERT INTO "T_OPBD_PERGUNTAS" ("id_pergunta", "ds_pergunta") VALUES (15, 'Qual tipo de escova, pasta de dente e produtos de higiene bucal você utiliza?');

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

-- Inserir respostas com conteúdo textual rico para análise
-- Respostas relacionadas a dor (para recomendação 4)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (1, 'Sim, tenho sentido uma dor forte e constante nos últimos dias, principalmente no lado direito da boca');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (2, 'Sim, sinto dor intensa quando mastigo alimentos ou bebo água gelada');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (3, 'Estou com uma dor pulsante que começou há três dias e está piorando');

-- Respostas relacionadas a sangramento gengival (para recomendação 5)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (4, 'Sim, minhas gengivas sangram quase todas as vezes que escovo os dentes');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (5, 'Às vezes noto sangue na escova e quando uso fio dental');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (6, 'Recentemente comecei a notar sangramento nas gengivas, principalmente pela manhã');

-- Respostas sobre hábitos de escovação (para recomendação 1)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (7, 'Escovo apenas uma vez por dia rapidamente antes de dormir');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (8, 'Duas vezes ao dia, mas acho que não estou fazendo corretamente pois sinto que ficam resíduos');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (9, 'Três vezes ao dia, após as refeições, por cerca de um minuto cada vez');

-- Respostas sobre uso de fio dental (para recomendação 2)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (10, 'Raramente uso fio dental, apenas quando sinto algo preso entre os dentes');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (11, 'Nunca adquiri o hábito de usar fio dental, acho desconfortável');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (12, 'Uso fio dental apenas nos finais de semana quando tenho mais tempo');

-- Respostas sobre sensibilidade (para recomendação 3)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (13, 'Sim, sinto muita sensibilidade ao tomar bebidas geladas ou sorvete');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (14, 'Tenho forte sensibilidade no lado esquerdo ao consumir alimentos ácidos ou gelados');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (15, 'Recentemente desenvolvi sensibilidade em vários dentes, especialmente com comidas doces');

-- Respostas sobre boca seca (para recomendação 7)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (16, 'Frequentemente sinto a boca seca, principalmente à noite e ao acordar');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (17, 'Sim, tenho boca seca constante e preciso beber água várias vezes durante o dia');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (18, 'Desde que comecei a tomar um novo medicamento, sinto minha boca constantemente seca');

-- Respostas sobre bruxismo (para recomendação 6)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (19, 'Sim, acordo com dor na mandíbula e meu parceiro diz que ranjo os dentes à noite');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (20, 'Tenho notado desgaste nos meus dentes e frequentemente acordo com dores de cabeça');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (21, 'Minha mandíbula estala quando abro a boca e sinto tensão muscular ao acordar');

-- Respostas sobre consumo de açúcar (para recomendação 8)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (22, 'Consumo refrigerantes e sucos diariamente, várias vezes ao dia');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (23, 'Como muitos doces e bebo café com açúcar várias vezes ao dia');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (24, 'Tenho o hábito de tomar bebidas cítricas frequentemente como limonada e suco de laranja');

-- Respostas sobre última consulta (para recomendação 0 e 9)
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (25, 'Minha última consulta foi há mais de 2 anos, não lembro o procedimento');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (26, 'Nunca fui ao dentista desde que me mudei para cá, há aproximadamente um ano');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (27, 'Faz uns 7 meses que fiz uma limpeza de rotina');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (28, 'Faço limpeza a cada 6 meses, a última foi há cerca de 5 meses');

-- Respostas neutras ou positivas
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (29, 'Não, não tenho sentido nenhuma dor ou desconforto');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (30, 'Minhas gengivas não sangram durante a escovação');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (31, 'Uso fio dental todos os dias, uma vez ao dia antes de dormir');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (32, 'Não sinto sensibilidade com alimentos quentes ou frios');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (33, 'Não tenho problema com boca seca');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (34, 'Não consumo refrigerantes ou doces com frequência');
INSERT INTO "T_OPBD_RESPOSTAS" ("id_resposta", "ds_resposta") VALUES (35, 'Não ranjo os dentes durante o sono');

-- PACIENTE 1: Perfil com problemas de DOR (recomendação 4)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (101, CURRENT_DATE-4, 1, 1, 1); -- Dor forte e constante

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (102, CURRENT_DATE-4, 1, 5, 14); -- Também tem sensibilidade

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (103, CURRENT_DATE-4, 1, 8, 25); -- Não vai ao dentista há mais de 2 anos

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (104, CURRENT_DATE-3, 1, 1, 2); -- Confirma dor ao mastigar

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (105, CURRENT_DATE-3, 1, 12, 29); -- Não tem inchaço

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (106, CURRENT_DATE-0, 1, 1, 3); -- Dor pulsante que está piorando

-- PACIENTE 2: Perfil com problemas de GENGIVITE (recomendação 5)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (201, CURRENT_DATE-4, 2, 2, 4); -- Gengivas sangram ao escovar

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (202, CURRENT_DATE-4, 2, 3, 8); -- Escovação inadequada

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (203, CURRENT_DATE-4, 2, 4, 10); -- Uso irregular de fio dental

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (204, CURRENT_DATE-2, 2, 2, 6); -- Sangramento recente nas gengivas

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (205, CURRENT_DATE-2, 2, 7, 29); -- Não tem mau hálito

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (206, CURRENT_DATE-0, 2, 2, 5); -- Confirma o sangramento

-- PACIENTE 3: Perfil com problemas de HIGIENE - ESCOVAÇÃO (recomendação 1)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (301, CURRENT_DATE-4, 3, 3, 7); -- Escova apenas uma vez por dia rapidamente

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (302, CURRENT_DATE-4, 3, 7, 29); -- Não tem problema de mau hálito

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (303, CURRENT_DATE-4, 3, 15, 28); -- Faz limpeza regularmente

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (304, CURRENT_DATE-2, 3, 3, 8); -- Confirma escovação inadequada

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (305, CURRENT_DATE-0, 3, 11, 29); -- Não tem manchas nos dentes

-- PACIENTE 4: Perfil com problemas de FIO DENTAL (recomendação 2)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (401, CURRENT_DATE-4, 4, 4, 11); -- Nunca usa fio dental

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (402, CURRENT_DATE-4, 4, 3, 9); -- Escova três vezes ao dia (bom)

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (403, CURRENT_DATE-4, 4, 2, 5); -- Tem algum sangramento

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (404, CURRENT_DATE-2, 4, 4, 10); -- Confirma uso irregular de fio dental

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (405, CURRENT_DATE-0, 4, 8, 27); -- Fez limpeza há 7 meses

-- PACIENTE 5: Perfil com SENSIBILIDADE (recomendação 3)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (501, CURRENT_DATE-4, 5, 5, 13); -- Forte sensibilidade

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (502, CURRENT_DATE-4, 5, 9, 24); -- Consome bebidas ácidas

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (503, CURRENT_DATE-4, 5, 3, 9); -- Escovação adequada

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (504, CURRENT_DATE-2, 5, 5, 15); -- Confirma sensibilidade com doces

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (505, CURRENT_DATE-0, 5, 1, 29); -- Não tem dor

-- PACIENTE 6: Perfil com BRUXISMO (recomendação 6)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (601, CURRENT_DATE-4, 6, 10, 19); -- Range os dentes

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (602, CURRENT_DATE-4, 6, 1, 29); -- Não tem dor

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (603, CURRENT_DATE-4, 6, 8, 27); -- Consulta recente

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (604, CURRENT_DATE-2, 6, 10, 20); -- Confirma desgaste nos dentes

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (605, CURRENT_DATE-0, 6, 10, 21); -- Mandíbula estala

-- PACIENTE 7: Perfil com BOCA SECA (recomendação 7)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (701, CURRENT_DATE-4, 7, 6, 16); -- Boca seca frequente

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (702, CURRENT_DATE-4, 7, 14, 18); -- Usa medicamento que causa boca seca

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (703, CURRENT_DATE-4, 7, 3, 9); -- Escovação adequada

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (704, CURRENT_DATE-2, 7, 6, 17); -- Confirma boca seca constante

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (705, CURRENT_DATE-0, 7, 7, 29); -- Não tem mau hálito

-- PACIENTE 8: Perfil com consumo excessivo de AÇÚCAR (recomendação 8)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (801, CURRENT_DATE-4, 8, 9, 22); -- Consome refrigerantes diariamente

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (802, CURRENT_DATE-4, 8, 11, 29); -- Não tem manchas nos dentes

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (803, CURRENT_DATE-4, 8, 3, 9); -- Escova bem

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (804, CURRENT_DATE-2, 8, 9, 23); -- Confirma consumo de doces

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (805, CURRENT_DATE-0, 8, 5, 15); -- Sensibilidade com doces

-- PACIENTE 9: Perfil para LIMPEZA PROFISSIONAL (recomendação 0)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (901, CURRENT_DATE-4, 9, 8, 27); -- Última limpeza há 7 meses

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (902, CURRENT_DATE-4, 9, 3, 9); -- Escova bem

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (903, CURRENT_DATE-4, 9, 4, 31); -- Usa fio dental

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (904, CURRENT_DATE-2, 9, 7, 29); -- Sem mau hálito

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (905, CURRENT_DATE-0, 9, 1, 29); -- Sem dor

-- PACIENTE 10: Perfil para EXAME COMPLETO (recomendação 9)
-- Dia 1
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (1001, CURRENT_DATE-4, 10, 8, 25); -- Não vai há mais de 2 anos

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (1002, CURRENT_DATE-4, 10, 13, 29); -- Sem dentes soltos

INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (1003, CURRENT_DATE-4, 10, 3, 8); -- Escovação inadequada

-- Dia 2
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (1004, CURRENT_DATE-2, 10, 8, 26); -- Confirma que nunca foi ao dentista na cidade atual

-- Dia 3
INSERT INTO "T_OPBD_CHECK_IN" ("id_check_in", "dt_check_in", "id_paciente", "id_pergunta", "id_resposta")
VALUES (1005, CURRENT_DATE-0, 10, 12, 29); -- Sem caroços na boca

-- COMMIT
COMMIT;