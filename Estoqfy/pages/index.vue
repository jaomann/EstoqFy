<template>
    <v-row class="justify-center" align="center" style="min-height: 100vh;">
      <v-col cols="12" md="6" lg="4">
        <v-row class="text-center mb-5">
          <v-col>
            <img
              src="/estoqfy.png"
              alt="Logo"
              class="mb-4"
              width="200"
              height="200"
            />
          </v-col>
        </v-row>
        
        <v-form>
          <v-text-field
            label="E-mail"
            type="email"
            v-model="email"
            :rules="emailRule"
            required
          ></v-text-field>
  
          <v-text-field
            label="Senha"
            type="password"
            v-model="password"
            :rules="passwordRule"
            required
          ></v-text-field>
  
          <v-btn color="primary" block v-on:click="handleLogin">Login</v-btn>
        </v-form>
        
        <v-row>
          <v-col class="text-center mt-2">
            <small>Não tem conta? <a v-on:click="register"> Registre-se.</a></small>
          </v-col>
        </v-row>
  
        <v-alert v-if="errorMessage" type="error" class="mt-3">
          {{ errorMessage }}
        </v-alert>
  
        <blockquote class="blockquote mt-5">
          Gerencie seu estoque de forma simples e direta.
          <footer>
            <small>
              <em>&mdash; João Emanuel, 11-2024</em>
            </small>
          </footer>
        </blockquote>
      </v-col>
    </v-row>
  </template>
  
  <script>
  export default {
    name: 'LoginPage',
    data() {
      return {
        email: '',
        password: '',
        errorMessage: '',
        emailRule: [
          v => !!v || 'E-mail é obrigatório',
          v => /.+@.+\..+/.test(v) || 'E-mail inválido'
        ],
        passwordRule: [
          v => !!v || 'Senha é obrigatória',
          v => v.length >= 6 || 'A senha deve ter pelo menos 6 caracteres'
        ]
      };
    },
    methods: {
      async handleLogin() {
        if (this.email && this.password) {
          this.errorMessage = '';
          const logged = await this.verifyLogin(this.email, this.password);
          if (logged) {
            this.$router.push('/home');
          } else {
            this.errorMessage = 'E-mail ou senha inválidos';
          }
        } else {
          this.errorMessage = 'Preencha todos os campos';
        }
      },
      async verifyLogin(email, password) {
        const response = await fetch("https://localhost:7182/api/auth/login", {
          method: "POST",
          body: JSON.stringify({ "Email": email, "Password": password }),
          headers: {
            "Content-Type": "application/json"
          }
        });
        if(response.ok)
          this.setCookie("userId", await response.text(), 8);

        return response.ok;
      },
      setCookie(name, value, hours) {
        const date = new Date();
        date.setTime(date.getTime() + (hours * 60 * 60 * 1000));
        const expires = "expires=" + date.toUTCString();
        document.cookie = `${name}=${value}; ${expires}; path=/`;
     },
      async register() {
        this.$router.push(`/register`);
      }
    }
  };
  </script>
  
  <style scoped>
  .v-row {
    background-color: #f5f5f5;
    padding: 20px;
  }
  
  .blockquote {
    font-style: italic;
    text-align: center;
  }
  </style>
  