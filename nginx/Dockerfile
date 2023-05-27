# syntax=docker/dockerfile:1

FROM nginx:1.21 as base
WORKDIR /etc/nginx/
EXPOSE 81

COPY *.conf .

# fail2ban depends on nginx, tail routes non-symlink access.log back into stdout
ENTRYPOINT nginx -g 'daemon off;'
