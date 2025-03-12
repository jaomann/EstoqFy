<template>
  <v-row align="center" class="container" style="min-height: 100vh;">
    <v-col>
      <v-card class="rounded-xl elevation-3 pa-4 mx-auto" style="width: 100%; max-width: 1200px;">
        <v-row>
          <v-col>
            <h1 class="display-1">Bem-vindo ao <b>Estoq</b><span style="color:greenyellow">fy</span>!</h1>
            <p class="text-subtitle">Gerencie seus itens de forma simples e eficiente.</p>
          </v-col>
        </v-row>

        <v-row align="center" justify="space-between" class="my-4">
          <v-col cols="8">
            <v-text-field
              v-model="search"
              label="Busca..."
              type="text"
              outlined
              dense
              class="rounded"
            ></v-text-field>
          </v-col>
          <v-col cols="4" class="text-right">
            <v-btn color="success" class="rounded-lg" @click="createItem">
              Criar Item
            </v-btn>
          </v-col>
        </v-row>

        <!-- Tabela de Itens -->
        <v-data-table
          :headers="headers"
          :items="filteredItems"
          item-key="EAN"
          class="elevation-1 rounded-xl"
          :search="search"
          :custom-filter="filterText"
        >
          <!-- eslint-disable vue/valid-v-slot -->
          <template #item.action="{ item }">
            <v-btn color="primary" small class="rounded" @click="editItem(item)">
              Editar
            </v-btn>
            <v-btn color="error" small class="rounded" @click="deleteItem(item)">
              Deletar
            </v-btn>
          </template>
          <!-- eslint-enable vue/valid-v-slot -->
        </v-data-table>
      </v-card>
    </v-col>
    <!-- Botão Sair no topo direito -->
    <div class="logout-button">
      <v-btn color="error" small class="rounded" @click="logOut()">
        Sair
      </v-btn>
    </div>
  </v-row>
</template>


<script>
import Swal from 'sweetalert2';

export default {
  name: 'HomePage',
  data() {
    return {
      search: '',
      items: [],
      filteredItems: [],
    };
  },
  computed: {
    headers() {
      return [
        { text: 'EAN', align: 'start', sortable: true, value: 'ean' },
        { text: 'Nome', align: 'start', value: 'name' },
        { text: 'Quantidade', align: 'center', value: 'count' },
        { text: 'Última Adição', align: 'center', value: 'lastAdd' },
        { text: 'Ações', align: 'center', value: 'action', sortable: false },
      ];
    },
  },
  mounted() {
    this.initialize();
  },
  methods: {
    logOut() {
      this.setCookie("userId", "", 1);
      this.$router.push(`/`);
    },
    filterText(value, search, item) {
      return value != null && search != null && typeof value === 'string' && value.toString().indexOf(search) !== -1;
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
    },
    setCookie(name, value, hours) {
      const date = new Date();
      date.setTime(date.getTime() + (hours * 60 * 60 * 1000));
      const expires = "expires=" + date.toUTCString();
      document.cookie = `${name}=${value}; ${expires}; path=/`;
    },
    async initialize() {
      var id = this.getCookie("userId");
      if (!id) this.$router.push(`/`);
      this.items = await this.getItems();
      this.filteredItems = this.items;
    },
    async getItems() {
      const response = await fetch(`/api/items?userId=${this.getCookie("userId")}`);
      if (response.ok) {
        const data = await response.json();
        return data.map(item => {
          if (item.lastAdd) {
            const date = new Date(item.lastAdd);
            if (!isNaN(date)) {
              item.lastAdd = date.toLocaleString('pt-BR', {
                weekday: 'short',
                year: 'numeric',
                month: '2-digit',
                day: '2-digit',
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
                hour12: false,
              });
            }
          }
          return item;
        });
      }
      return [];
    },
    editItem(item) {
      this.$router.push(`/edit/${item.id}`);
    },
    createItem() {
      this.$router.push(`/create`);
    },
    deleteItem(item) {
      Swal.fire({
        title: 'Deletar Item?',
        text: 'Você tem certeza que deseja remover esse item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Sim, tenho!',
        cancelButtonText: 'Não',
      }).then(async (result) => {
        if (result.isConfirmed) {
          const success = await this.fetchDelete(item.id);
          if (success) {
            this.filteredItems = await this.getItems();
          }
        }
      });
    },
    async fetchDelete(id) {
      const response = await fetch(`/api/item?id=${id}`, { method: 'DELETE' });
      return response.ok;
    },
  },
};
</script>

<style scoped>
.v-row {
  background-color: #121212;
  padding: 20px;
  color: white;
}

.display-1 {
  font-size: 3rem;
  font-weight: 700;
  color: #ffffff;
  margin-bottom: 10px;
}

.text-subtitle {
  font-size: 1.2rem;
  color: #bbbbbb;
}

.v-btn {
  color: white;
  font-weight: bold;
  text-transform: uppercase;
  padding: 8px 16px;
}

.v-btn.success {
  background-color: #4caf50;
}

.v-btn.primary {
  background-color: #1e88e5;
}

.v-btn.error {
  background-color: #e53935;
}

.v-card {
  background-color: #1e1e1e;
  border-radius: 16px;
  box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.4);
}

.v-data-table th {
  background-color: #2c2c2c;
  color: white;
  text-align: center;
}

.v-data-table td {
  color: white;
  text-align: center;
}

.v-data-table tbody tr:hover {
  background-color: #3c3c3c;
}

.v-text-field {
  background-color: #2c2c2c;
  border-radius: 8px;
}

.v-text-field input {
  color: white;
}

/* Estilo para posicionar o botão "Sair" no topo direito */
.logout-button {
  position: absolute;
  top: 20px;
  right: 20px;
}
</style>
