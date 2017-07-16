exports.addPen = (msg = '') => msg + 'pen'
exports.addApple = (msg = '') => msg + 'Apple'
exports.addSpace = (msg = '') => msg + ' '

exports.comp1 = (f, g) => x => f(g(x))
exports.comp = (...funs) => x => funs.reduceRight((y, f) => f(y), x)
exports.pipe = (...funs) => x => funs.reduce((y, f) => f(y), x)

exports.inc = x => x + 1
exports.dup = x => x * 2
exports.normal_sum = (x, y) => x + y

exports.sum = x => y => x + y

// Identity container
const Composition = x => ({
  map: f => Composition(f(x)), // then, pipe, select
  return: () => x,
  done: f => f(x)
})

Composition.of = x => Composition(x)

exports.Composition = Composition
