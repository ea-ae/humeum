 module.exports = {
    purge: {
        content: ['./src/**/*.{html,js,jsx,ts,tsx}'], // /dist/
        options: {
            safelist: []
        }
    },
    theme: {
        extend: {},
        fontFamily: {
            'open-sans': 'Open Sans, sans-serif',
        },
    },
    plugins: [],
}
