# Estágio de build
FROM node:18 AS build
WORKDIR /app
COPY package.json yarn.lock ./
RUN yarn install
COPY . .
RUN yarn nuxt generate  # Gera os arquivos estáticos na pasta dist

# Estágio final
FROM node:18 AS final
WORKDIR /app
RUN yarn global add http-server
COPY --from=build /app/dist /app/dist
EXPOSE 3000
CMD ["http-server", "dist", "-p", "3000"]
