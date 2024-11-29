<template>
  <v-row class="justify-center" align="center" style="min-height: 100vh;">
    <v-col cols="12" md="8" lg="6">
      <v-card class="rounded-xl elevation-3">
        <v-card-title class="text-h5 justify-center">Editar Item</v-card-title>

        <v-card-subtitle class="text-center">
          Preencha os campos abaixo para editar o item.
        </v-card-subtitle>

        <v-form v-if="item && item.id" @submit.prevent="updateItem" class="pa-4">
          <v-text-field
            v-model="item.ean"
            label="EAN"
            :rules="['required']"
            maxlength="13"
            type="number"
            required
            outlined
            class="rounded"
          ></v-text-field>

          <v-text-field
            v-model="item.name"
            label="Nome"
            :rules="['required']"
            required
            outlined
            class="rounded"
          ></v-text-field>

          <v-text-field
            v-model="item.count"
            label="Quantidade"
            type="number"
            :rules="['required', 'min:1']"
            required
            outlined
            class="rounded"
          ></v-text-field>

          <v-text-field
            v-model="item.lastAdd"
            label="Última Adição"
            type="datetime-local"
            :rules="['required']"
            disabled
            outlined
            class="rounded"
          ></v-text-field>

          <v-btn color="primary" type="submit" class="rounded-lg mt-3">Salvar</v-btn>
          <v-btn @click="cancelEdit" color="secondary" class="rounded-lg mt-3">Cancelar</v-btn>
        </v-form>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
export default {
  name: 'EditItem',
  data() {
    return {
      item: {
        ean: '',
        name: '',
        count: 0,
        lastAdd: '',
        userId: ''
      },
    };
  },
  async mounted() {
    const id = this.$route.params.id;
    if (id) {
      await this.getItem(id);
    }
  },
  methods: {
    async getItem(id) {
      const response = await fetch(`https://localhost:32769/api/item?id=${id}`);
      if (response.ok) {
        const data = await response.json();
        this.item = data;
      } else {
        console.error('Erro ao carregar item');
      }
    },

    async updateItem() {
      const response = await fetch(`https://localhost:32769/api/item?EAN=${this.item.ean}&userId=${this.item.userId}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(this.item),
      });

      if (response.ok) {
        this.$router.push('/home');
      } else {
        console.error('Erro ao atualizar item');
      }
    },

    cancelEdit() {
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
  border-radius: 16px;
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.4);
}

.v-btn {
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
  text-align: center;
}

.v-card-subtitle {
  margin-bottom: 16px;
  font-size: 0.9rem;
  text-align: center;
}

.v-text-field {
  background-color: #2c2c2c;
  border-radius: 8px;
}

.v-text-field input {
  color: white;
}
</style>
