/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    screens: {
      sm: "480px",
      md: "760px",
      lg: "976px",
      xl: "1440px",
    },
    fontFamily: {
      Poppins: ["Poppins", "sans-serif"],
    },
    extend: {
      colors: {
        primaryYellow: "#FFB928FF",
        subtleGreen: "#00846BFF",
        lightGreen: "#D5F6EFFF",
        lightBrown: "#FFE8E8FF",
      },
    },
  },
  plugins: [],
};
