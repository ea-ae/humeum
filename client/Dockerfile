FROM node:19-alpine AS base
EXPOSE 9001
VOLUME /app/dist

WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH

# clean install packages
COPY package*.json ./
RUN npm ci

# build with webpack
COPY . ./
CMD npm run prod
