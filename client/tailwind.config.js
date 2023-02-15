 module.exports = {
    purge: {
        content: ['./src/**/*.{html,js,jsx,ts,tsx}'], // /dist/
        options: {
            safelist: []
        }
    },
    theme: {
        extend: {
            colors: {
                primary: {
                    200: '#24FFA4',
                    300: '#20E693',
                    400: '#1FD68A',
                    500: '#19B071',
                    600: '#0C5738',
                },
                secondary: {
                    400: '#DB5756',
                },
                tertiary: {
                    500: '#078DA3',
                },
                white: '#fff',
            }
        },
        fontFamily: {
            'open-sans': 'Open Sans, sans-serif',
        },
    },
    plugins: [],
    important: true,
}
