# Estágio de build
FROM node:18 AS build
WORKDIR /app
COPY package.json yarn.lock ./
RUN yarn install
COPY . .
RUN yarn build

# Estágio final
FROM node:18 AS final
WORKDIR /app
RUN yarn global add http-server
COPY --from=build /app/.output /app/.output
EXPOSE 3000
CMD ["http-server", ".output/public", "-p", "3000"]
