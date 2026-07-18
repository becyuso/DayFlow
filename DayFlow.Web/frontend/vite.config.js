import { defineConfig } from 'vite';
import path from 'node:path';
export default defineConfig({
    appType: "custom",
    server: {
        port: 5173,
        strictPort: true
    },
    build: {
        outDir: '../wwwroot/dist',
        emptyOutDir: true,
        manifest: true,
        rollupOptions: {
            input: {
                app: "./app/main.ts",
                editor: "./app/editor.ts"
            }
        }
    },
    resolve: {
        alias: {
            '@': path.resolve(__dirname)
        }
    }
});
//# sourceMappingURL=vite.config.js.map