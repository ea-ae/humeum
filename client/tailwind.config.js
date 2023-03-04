module.exports = {
  purge: {
    content: ['./src/**/*.{html,js,jsx,ts,tsx}'], // /dist/
    options: {
      safelist: [],
    },
  },
  theme: {
    extend: {
      colors: {
        primary: {
          50: '#F6FCF2', // #F2FAED
          100: '#EAF2E6', // #DFEDD8
          200: '#24FFA4',
          300: '#20E693',
          400: '#1FD68A',
          500: '#19B071',
          600: '#0C5738',
          // emerald
          // 400: '#13BF63',
          // 500: '#349965'
        },
        secondary: {
          // 400: '#DB5756',
          400: '#E6BD70',
          500: '#CCA864',
        },
        tertiary: {
          // 500: '#078DA3',
          // purple
          // 300: '#C663FF',
          // 400: '#B25AE6',
          // 500: '#A25ACC',
          // 600: '#7A4399',
          // blue
          200: '#5A31FF',
          300: '#512CE6',
          400: '#4324BF',
          500: '#2D1880',
        },
        white: '#FFF',
      },
    },
    fontFamily: {
      'open-sans': 'Open Sans, sans-serif',
    },
  },
  plugins: [],
  important: true,
};
