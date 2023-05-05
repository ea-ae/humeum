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
          50: '#F6FCF2', // used for saturated card backgrounds
          100: '#EAF2E6', // used for saturated neutral backgorunds
          150: '#E0EBDA', // used for card skeletons
          200: '#24FFA4',
          300: '#20E693',
          400: '#1FD68A',
          600: '#0C5738',
          500: '#19B071',
          // emerald
          // 400: '#13BF63',
          // 500: '#349965'
        },
        secondary: {
          // 400: '#DB5756',
          100: '#FCF5DE', // used for saturated sidebar background
          400: '#E6BD70', // used for selected sidebar item
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
          300: '#512CE6', // used for buttons
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
