import { defineConfig } from 'vite';
import path from 'path';
export default defineConfig({
    build: {
        outDir: 'wwwroot/dist',
        emptyOutDir: true,
        manifest: true,
        rollupOptions: {
            input: {
                editor: 'frontend/app/editor.ts'
            }
        }
    },
    resolve: {
        alias: {
            '@': path.resolve(__dirname, 'frontend')
        }
    }
});
//# sourceMappingURL=vite.config.js.map