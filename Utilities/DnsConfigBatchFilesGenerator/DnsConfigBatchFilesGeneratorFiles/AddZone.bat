
ECHO ***************** Adding Zone %8
SET PRIMARY_SERVER_NAME=%1
SET PRIMARY_DOMAIN=%2
SET PRIMARY_IP=%3
SET SECONDRY_SERVER_NAME=%4
SET SECONDRY_DOMAIN=%5
SET SECONDRY_IP=%6
SET SOA_EMAIL=%7
SET DOMAIN_NAME=%8

dnscmd %PRIMARY_SERVER_NAME% /zonedelete %DOMAIN_NAME% /f
dnscmd %PRIMARY_SERVER_NAME% /zoneadd %DOMAIN_NAME% /Primary /file %DOMAIN_NAME%.dns
dnscmd %PRIMARY_SERVER_NAME% /recordadd %DOMAIN_NAME% @ SOA %PRIMARY_DOMAIN%. %SOA_EMAIL%. 1 10800 3600 2592000 86400
dnscmd %PRIMARY_SERVER_NAME% /recorddelete %DOMAIN_NAME% @ NS %PRIMARY_SERVER_NAME%. /f
dnscmd %PRIMARY_SERVER_NAME% /recordadd %DOMAIN_NAME% @ NS %PRIMARY_DOMAIN%.
dnscmd %PRIMARY_SERVER_NAME% /recordadd %DOMAIN_NAME% @ NS %SECONDRY_DOMAIN%.
dnscmd %PRIMARY_SERVER_NAME% /recordadd %DOMAIN_NAME% %PRIMARY_DOMAIN%. A %PRIMARY_IP%
dnscmd %PRIMARY_SERVER_NAME% /recordadd %DOMAIN_NAME% %SECONDRY_DOMAIN%. A %SECONDRY_IP%
dnscmd %PRIMARY_SERVER_NAME% /writebackfiles %DOMAIN_NAME%
