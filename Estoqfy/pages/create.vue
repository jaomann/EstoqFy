<template>
    <v-row class="justify-center" align="center" style="min-height: 100vh;">
      <v-col cols="12" md="8" lg="6">
        <v-card class="rounded-xl elevation-3">
          <v-card-title class="text-h5 justify-center">Criar Item</v-card-title>
  
          <v-card-subtitle class="text-center">
            Preencha os campos abaixo para criar um item.
          </v-card-subtitle>
  
          <v-form @submit.prevent="createItem" class="pa-4">
            <v-text-field
              v-model="item.ean"
              label="EAN"
              :rules="[eanLengthRule]"
              required
              type="text"
              outlined
              class="rounded"
            ></v-text-field>
  
            <v-text-field
              v-model="item.name"
              max-length="45"
              label="Nome"
              required
              outlined
              class="rounded"
            ></v-text-field>
  
            <v-text-field
                v-model="item.count"
                label="Quantidade"
                type="number"
                :rules="[countRule]"
                @input="handleCountInput"
                required
                outlined
                class="rounded"
            />
  
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
    name: 'CreateItem',
    data() {
      return {
        item: {
            id: "00000000-0000-0000-0000-000000000000",
            ean: '',
            name: '',
            count: 0,
            lastAdd: new Date().toISOString(),
            userId: '',
            isDeleted: false
        },
      };
    },
    computed: {
        eanLengthRule() {
            return value => {
            if (value && (value.length > 13 || value.length < 8)) {
                return 'O EAN deve ter entre 8 e 13 dígitos.';
            }
            return true;
            };
        },
        countRule() {
            return value => {
            if (value < 0) {
                return 'A quantidade não pode ser negativa.';
            }
            return true;
            };
        },
    },
    methods: {
        async createItem() {
            this.item.userId = this.getCookie("userId");
            console.log(this.item);
            const response = await fetch(`https://localhost:7182/api/item`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(this.item),
        });
  
        if (response.ok) {
          this.$router.push('/home');
        } else {
          console.error('Erro ao criar item');
        }
      },
      cancelCreate() {
        this.item = { ean: '', name: '', count: 1 };
        this.$router.push('/home');
      },
      getCookie(name) {
        const cookies = document.cookie.split(';');
        for (let i = 0; i < cookies.length; i++) {
          let cookie = cookies[i].trim();
          if (cookie.indexOf(name + "=") === 0) {
            return cookie.substring(name.length + 1);
          }
        }
        return null;
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
  border-radius: 16px; /* Bordas arredondadas */
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.4); /* Sombra suave */
}

.v-btn {
  margin-top: 20px;
  font-weight: bold;
  color: white;
}

.v-btn.primary {
  background-color: #4caf50; /* Verde moderno */
}

.v-btn.secondary {
  background-color: #ff5722; /* Laranja moderno */
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

  