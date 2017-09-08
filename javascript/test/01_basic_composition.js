
const {expect} = require('chai')
const {stream} = require('flyd')

const { addPen, addApple, addSpace, comp1, comp, inc, dup, Composition } = require('../basic.js')

describe('Composition', () => {
  describe('Pointless compostion', () => {
    it('Basic', () => {
      const applePen = comp1(addPen, addApple)
      expect(applePen()).to.equal('Applepen')
    })
    it('Basic tree functions compostion',
      () => {
        const applePen = comp1(addPen, comp1(addSpace, addApple))
        expect(applePen()).to.equal('Apple pen')
      })
    it('Normal comp', () => {
      expect(comp(addPen, addSpace, addApple)('')).to.equal('Apple pen')
    })
  })

  describe('Algebraic compostion', () => {
    it('Identity with return', () => {
      const apComp = Composition.of()
                        .map(addApple)
                        .map(addSpace)
                        .map(addPen)
      expect(apComp.return(), 'Apple pen')
    })

    it('Identity without context exit', () => {
      Composition.of()
          .map(addApple)
          .map(addSpace)
          .map(addPen)
          .done(x => expect(x).to.be.equal('Apple pen'))
    })

    it('Numeric example', () => {
      Composition.of(5)
          .map(inc)
          .map(dup)
          .map(inc)
          .done(x => expect(x).to.be.equal(13))
    })
    it('Array Composition', () => {
      Array.prototype.done = function (cb) { cb(this) }  // eslint-disable-line
      Array.of(5, 7)
          .map(inc)
          .map(dup)
          .map(inc)
          .done(x => expect(x).to.deep.equal([13, 17]))
    })
    it('Stream Composition', () => {
      var s = stream(5)
            .map(inc)
            .map(dup)
            .map(inc)
      expect(s()).to.be.equal(13)
    })
  })
})
