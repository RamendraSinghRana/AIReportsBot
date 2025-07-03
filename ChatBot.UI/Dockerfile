FROM node:20-alpine as build
WORKDIR /app
COPY . .
RUN npm install && npm run build --prod

FROM nginx:alpine
COPY --from=build /app/dist/ChatBot.UI /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]