# Usa a imagem oficial do Nginx
FROM nginx:latest

# Remove o arquivo de configuração padrão
RUN rm /etc/nginx/conf.d/default.conf

# Copia o arquivo de configuração personalizado
COPY ./conf.d/default.conf /etc/nginx/conf.d/default.conf

# Expõe a porta 80
EXPOSE 80

# Comando para rodar o Nginx
CMD ["nginx", "-g", "daemon off;"]