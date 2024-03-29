
SET PRIMARY_SERVER_NAME=%1
SET PRIMARY_DOMAIN=%2
SET PRIMARY_IP=%3
SET SECONDRY_SERVER_NAME=%4
SET SECONDRY_DOMAIN=%5
SET SECONDRY_IP=%6
SET SOA_EMAIL=%7
SET DOMAIN_NAME=%8
SET IP_ADDRESS=%9
SHIFT
SET CNAME=%9

CALL AddZone %PRIMARY_SERVER_NAME% %PRIMARY_DOMAIN% %PRIMARY_IP% %SECONDRY_SERVER_NAME% %SECONDRY_DOMAIN% %SECONDRY_IP% %SOA_EMAIL% %DOMAIN_NAME%

ECHO dnscmd %PRIMARY_SERVER_NAME% /recordadd %DOMAIN_NAME% @ CNAME %CNAME%.
ECHO dnscmd %PRIMARY_SERVER_NAME% /recordadd %DOMAIN_NAME% www CNAME %CNAME%.
dnscmd %PRIMARY_SERVER_NAME% /recordadd %DOMAIN_NAME% @ A %IP_ADDRESS%
dnscmd %PRIMARY_SERVER_NAME% /recordadd %DOMAIN_NAME% www A %IP_ADDRESS%
dnscmd %PRIMARY_SERVER_NAME% /writebackfiles %DOMAIN_NAME%

CALL AddSecondryZone %PRIMARY_SERVER_NAME% %PRIMARY_DOMAIN% %PRIMARY_IP% %SECONDRY_SERVER_NAME% %SECONDRY_DOMAIN% %SECONDRY_IP% %DOMAIN_NAME%
