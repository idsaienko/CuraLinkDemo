import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import path from "path";

export default defineConfig({
    plugins: [plugin()],
    server: {
        port: 57378,
    },
    resolve: {
        alias: {
            "@": path.resolve(__dirname, "./src"),
        },
    },
})
