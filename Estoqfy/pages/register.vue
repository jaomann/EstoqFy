<template>
    <v-row class="justify-center" align="center" style="min-height: 100vh;">
      <v-col cols="12" md="8" lg="6">
        <v-card class="rounded-xl elevation-3">
          <v-card-title class="text-h5 justify-center">Registro</v-card-title>
  
          <v-card-subtitle class="text-center">
            Preencha os campos abaixo para se registrar no Estoqfy.
          </v-card-subtitle>
  
          <v-form @submit.prevent="createUser" class="pa-4">
            <v-text-field
              v-model="user.username"
              label="Nome de Usuário"
              required
              max-length="45"
              outlined
              class="rounded"
            ></v-text-field>
  
            <v-text-field
                label="E-mail"
                type="email"
                v-model="user.email"
                :rules="emailRule"
                outlined
                class="rounded"
                required
            ></v-text-field>
  
            <v-text-field
                label="Senha"
                type="password"
                v-model="user.password"
                :rules="passwordRule"
                outlined
                class="rounded"
                required
            ></v-text-field>
  
            <v-text-field
              v-model="user.cnpj"
              label="CNPJ"
              required
              max-length="18"
              outlined
              class="rounded"
            ></v-text-field>
  
            <v-btn block color="primary" type="submit" class="rounded-lg mt-3">
              Criar
            </v-btn>
            <v-btn block @click="cancelCreate" color="secondary" class="rounded-lg mt-3">
              Cancelar
            </v-btn>
          </v-form>
        </v-card>
      </v-col>
    </v-row>
  </template>
  
  <script>
  export default {
    name: 'CreateUser',
    data() {
      return {
        user: {
            username: '',
            email: '',
            password: '',
            cnpj: '',
            create_time: new Date().toISOString()
        },
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
      async createUser() {
        console.log(this.user); 
        const response = await fetch('https://localhost:5000/api/user', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(this.user),
        });
  
        if (response.ok) {
          this.$router.push('/home');
        } else {
          console.error('Erro ao criar usuário');
        }
      },
      cancelCreate() {
        this.user = {
          username: '',
          email: '',
          password: '',
          cnpj: '',
          create_time: new Date().toISOString(),
        };
        this.$router.push('/home');
      }
    }
  };
  </script>
  
  <style scoped>
  .v-row {
    background-color: #121212;
    padding: 20px;
    color: white;
  }
  
  .v-card {
    background-color: #1e1e1e;
    color: white;
    border-radius: 16px;
    box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.4);
  }
  
  .v-btn {
    margin-top: 20px;
    font-weight: bold;
    color: white;
  }
  
  .v-btn.primary {
    background-color: #4caf50;
  }
  
  .v-btn.secondary {
    background-color: #ff5722;
  }
  
  .v-text-field input {
    color: white;
  }
  
  .v-card-title,
  .v-card-subtitle {
    color: white;
  }
  
  .v-card-title {
    font-weight: bold;
    text-transform: uppercase;
    letter-spacing: 1px;
  }
  
  .v-card-subtitle {
    margin-bottom: 16px;
    font-size: 0.9rem;
  }
  </style>
  