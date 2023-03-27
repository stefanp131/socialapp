/** @type {import('tailwindcss').Config} */
module.exports = {
  important: true,
  content: ["./src/**/*.{html,ts}"],
  purge: {
    enabled: true,
    content: ["./src/**/*.html", "./src/**/*.scss"],
  },
  theme: {
    extend: {},
  },
  plugins: [],
};
