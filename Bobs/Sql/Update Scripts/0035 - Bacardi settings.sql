delete from Setting where Name in ('BacardiIconImage', 'BacardiIconLink')
INSERT INTO Setting (Name, Value) VALUES ('BacardiIconImage', '')
INSERT INTO Setting (Name, Value) VALUES ('BacardiIconLink', '')
