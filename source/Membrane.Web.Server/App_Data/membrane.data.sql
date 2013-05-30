BEGIN TRANSACTION;

DROP TABLE IF EXISTS DemoValue;
CREATE TABLE DemoValue (ID TEXT, Code TEXT, Description TEXT, Value TEXT, CreationTimestamp TEXT, ValidFrom TEXT, ValidUntil TEXT);
CREATE UNIQUE INDEX IDX_DemoValue ON DemoValue (ID);
CREATE UNIQUE INDEX IDX_DemoValue_CODE ON DemoValue (Code);
INSERT INTO DemoValue (ID, Code, Description, Value, CreationTimestamp, ValidFrom, ValidUntil) VALUES ("00000000-0000-0000-0000-000000000001", "VALUE01", "Value 01", "VALUE01", "2013-01-01", "1900-01-01", "2100-01-01");
INSERT INTO DemoValue (ID, Code, Description, Value, CreationTimestamp, ValidFrom, ValidUntil) VALUES ("00000000-0000-0000-0000-000000000002", "VALUE02", "Value 02", "VALUE02", "2013-01-01", "1900-01-01", "2100-01-01");

DROP TABLE IF EXISTS OAuth2CryptoKey;
CREATE TABLE OAuth2CryptoKey (ID TEXT, Bucket TEXT, Handle TEXT, Secret TEXT, ExpiresUtc TEXT);
CREATE UNIQUE INDEX IDX_OAuth2CryptoKey ON OAuth2CryptoKey (ID);

DROP TABLE IF EXISTS OAuth2Nonce;
CREATE TABLE OAuth2Nonce (ID TEXT, Context TEXT, Code TEXT, Timestamp TEXT);
CREATE UNIQUE INDEX IDX_OAuth2Nonce ON OAuth2Nonce (ID);

DROP TABLE IF EXISTS OAuth2ClientApplication;
CREATE TABLE OAuth2ClientApplication (ID TEXT, Description TEXT, Identifier TEXT, Secret TEXT, CreationTimestamp TEXT, ValidFrom TEXT, ValidUntil TEXT);
CREATE UNIQUE INDEX IDX_OAuth2ClientApplication ON OAuth2ClientApplication (ID);
INSERT INTO OAuth2ClientApplication (ID, Description, Identifier, Secret, CreationTimestamp, ValidFrom, ValidUntil) VALUES ("00000000-0000-0000-0000-000000000001", "Demo application", "DEMO-APPLICATION", "DEMO-APPLICATION-SECRET", "2013-01-01", "1900-01-01", "2100-01-01");

DROP TABLE IF EXISTS OAuth2User;
CREATE TABLE OAuth2User (ID TEXT, Identifier TEXT, Password TEXT, IsAdministrator INT, CreationTimestamp TEXT, ValidFrom TEXT, ValidUntil TEXT);
CREATE UNIQUE INDEX IDX_OAuth2User ON OAuth2User (ID);
CREATE UNIQUE INDEX IDX_OAuth2User_Identifier ON OAuth2User (Identifier);
INSERT INTO OAuth2User (ID, Identifier, Password, IsAdministrator, CreationTimestamp, ValidFrom, ValidUntil) VALUES ("00000000-0000-0000-0000-000000000001", "demo@demo.com", "demo", 1, "2013-01-01", "1900-01-01", "2100-01-01");

DROP TABLE IF EXISTS OAuth2Scope;
CREATE TABLE OAuth2Scope (ID TEXT, Code TEXT, Description TEXT, CreationTimestamp TEXT, ValidFrom TEXT, ValidUntil TEXT);
CREATE UNIQUE INDEX IDX_OAuth2Scope ON OAuth2Scope (ID);
INSERT INTO OAuth2Scope (ID, Code, Description, CreationTimestamp, ValidFrom, ValidUntil) VALUES ("00000000-0000-0000-0000-000000000002", "application.role.client.default", "Default client role", "2013-01-01", "1900-01-01", "2100-01-01");
INSERT INTO OAuth2Scope (ID, Code, Description, CreationTimestamp, ValidFrom, ValidUntil) VALUES ("00000000-0000-0000-1000-000000000002", "application.role.user.default", "Default user role", "2013-01-01", "1900-01-01", "2100-01-01");
INSERT INTO OAuth2Scope (ID, Code, Description, CreationTimestamp, ValidFrom, ValidUntil) VALUES ("00000000-0000-0000-1000-000000000003", "application.role.user.administrator", "Administrator user role", "2013-01-01", "1900-01-01", "2100-01-01");

DROP TABLE IF EXISTS OAuth2UserScope;
CREATE TABLE OAuth2UserScope (ID TEXT, UserID TEXT, ScopeID TEXT, CreationTimestamp TEXT, ValidFrom TEXT, ValidUntil TEXT);
CREATE UNIQUE INDEX IDX_OAuth2UserScope ON OAuth2UserScope (ID);
CREATE UNIQUE INDEX IDX_OAuth2UserScope_UserID_ScopeID ON OAuth2UserScope (UserId, ScopeID);
INSERT INTO OAuth2UserScope (ID, UserID, ScopeID, CreationTimestamp, ValidFrom, ValidUntil) VALUES ("00000000-0000-0000-1000-000000000002", "00000000-0000-0000-0000-000000000001", "00000000-0000-0000-1000-000000000002", "2013-01-01", "1900-01-01", "2100-01-01");
INSERT INTO OAuth2UserScope (ID, UserID, ScopeID, CreationTimestamp, ValidFrom, ValidUntil) VALUES ("00000000-0000-0000-1000-000000000003", "00000000-0000-0000-0000-000000000001", "00000000-0000-0000-1000-000000000003", "2013-01-01", "1900-01-01", "2100-01-01");

DROP TABLE IF EXISTS Identity;
CREATE TABLE Identity (ID TEXT, Name TEXT, FirstName TEXT, Locale TEXT, CreationTimestamp TEXT);
CREATE UNIQUE INDEX IDX_Identity ON Identity (ID);
INSERT INTO Identity (ID, Name, FirstName, Locale, CreationTimestamp) VALUES ("00000000-0000-0000-0000-000000000001", "Demo", "User", "en_US", "2013-01-01");

DROP TABLE IF EXISTS OAuth2UserIdentity;
CREATE TABLE OAuth2UserIdentity (ID TEXT, UserID TEXT, IdentityID TEXT, CreationTimestamp TEXT, ValidFrom TEXT, ValidUntil TEXT);
CREATE UNIQUE INDEX IDX_OAuth2UserIdentity ON OAuth2UserIdentity (ID);
CREATE UNIQUE INDEX IDX_OAuth2UserIdentity_UserID_ScopeID ON OAuth2UserIdentity (UserId, IdentityID);
INSERT INTO OAuth2UserIdentity (ID, UserID, IdentityID, CreationTimestamp, ValidFrom, ValidUntil) VALUES ("00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000001", "2013-01-01", "1900-01-01", "2100-01-01");

COMMIT;
