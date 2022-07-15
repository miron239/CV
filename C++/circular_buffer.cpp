#include <iterator>
#include <memory>
#include <stdexcept>


using namespace std;

    template < typename T, typename A = std::allocator<T> >
    class circular_buffer
    {
    public:
        typedef circular_buffer<T, A> self_type;
        typedef A allocator_type;
        typedef typename allocator_type::value_type       value_type;
        typedef typename allocator_type::pointer      pointer;
         typedef typename allocator_type::const_pointer     const_pointer;
           typedef typename allocator_type::reference         reference;
       typedef typename allocator_type::const_reference   const_reference;

           typedef typename allocator_type::size_type         size_type;
           typedef typename allocator_type::difference_type   difference_type;

           ~circular_buffer()
           {
                  clear();
                  m_allocator.deallocate( m_array, m_capacity );
               }

           explicit circular_buffer( size_type const capacity = 1,
                                     allocator_type const & allocator = allocator_type() )
           : m_capacity( capacity )
           , m_allocator( allocator )
           , m_array( m_allocator.allocate( capacity, (void *) 0 ) )
           , m_head(1)
           , m_tail(0)
           , m_contents_size(0)
           {
               }


           circular_buffer( self_type const & other )
           : m_capacity( other.m_capacity )
           , m_allocator( other.m_allocator )
           , m_array( m_allocator.allocate( other.m_capacity, (void *) 0 ) )
           , m_head( other.m_head )
           , m_tail( other.m_tail )
           , m_contents_size(other.m_contents_size )
           {
                  try
                  {
                         assign_into( other.begin(), other.end() );
                      }
                  catch ( ... )
                  {
                         clear();
                         m_allocator.deallocate( m_array, m_capacity );
                         throw;
                      }
               }

           template < typename II >
           circular_buffer(II const from, II const to)
           : m_capacity( 0 )
           , m_allocator( allocator_type() )
           , m_array( 0 )
           , m_head( 0 )
           , m_tail( 0 )
           , m_contents_size( 0 )
           {
                  circular_buffer tmp( std::distance( from, to ) );
                  tmp.assign_into( from, to );
                  swap( tmp );
               }

           circular_buffer & operator=( self_type other )
           {
                  other.swap( *this );
                  return *this;
               }

           void swap( self_type & other )
           {
                  using std::swap;

                  swap( m_capacity,      other.m_capacity );
                  swap( m_array,         other.m_array );
                  swap( m_head,          other.m_head );
                  swap( m_tail,          other.m_tail );
                  swap( m_contents_size, other.m_contents_size );
               }

           allocator_type get_allocator() const
           {
                  return m_allocator;
               }


           template< typename E, typename EN >
           class iterator_ : public std::iterator< std::random_access_iterator_tag, EN >
           {
               public:
                  typedef E elem_type;

                  typedef EN elem_type_nc;

                  typedef iterator_< elem_type, elem_type_nc > self_type;

                  typedef circular_buffer<T,A> circular_buffer_type;

#ifdef SPM_CORE_CIRCULAR_BUFFER_COMPILER_IS_MSVC6
                  typedef typename circular_buffer_type::difference_type difference_type;
#endif


                  iterator_( circular_buffer_type * const buf, size_type const pos )
                  : m_buf( buf )
                  , m_pos( pos )
                  {
                      }

                  iterator_( circular_buffer_type const * const buf, size_type const pos )
                  : m_buf( const_cast<circular_buffer_type * >( buf ) )
                  , m_pos( pos )
                  {
                      }


                  friend class iterator_<const elem_type, elem_type_nc> ;

                  iterator_( iterator_<elem_type_nc, elem_type_nc> const & other )
                  : m_buf( other.m_buf )
                  , m_pos( other.m_pos )
                  {
                      }

                  self_type & operator= ( self_type const & other )
                  {
                         m_buf = other.m_buf;
                         m_pos = other.m_pos;
                         return *this;
                      }



                  elem_type & operator*()
                  {
                         return ( *m_buf )[ m_pos ];
                      }

                  elem_type * operator->()
                  {
                         return &( operator*() );
                      }



                  self_type & operator++()
                  {
                         m_pos += 1;
                         return *this;
                      }

                  self_type operator++( int )
                  {
                         self_type tmp( *this );
                         ++( *this );
                         return tmp;
                      }

                  self_type & operator--()
                  {
                         m_pos -= 1;
                         return *this;
                      }

                  self_type operator--( int )
                  {
                         self_type tmp( *this );
                         --( *this );
                         return tmp;
                      }

                  self_type operator+ ( difference_type const n ) const
                  {
                         self_type tmp( *this );
                         tmp.m_pos += n;
                         return tmp;
                      }

                  self_type &operator+= ( difference_type const n )
                  {
                         m_pos += n;
                         return *this;
                      }

                  self_type operator- ( difference_type const n ) const
                  {
                         self_type tmp( *this );
                         tmp.m_pos -= n;
                         return tmp;
                      }

                  self_type &operator-= ( difference_type const n )
                  {
                         m_pos -= n;
                         return *this;
                      }

                  difference_type operator- ( self_type const & c ) const
                  {
                         return m_pos - c.m_pos;
                      }

                  bool operator== ( self_type const & other) const
                  {
                         return m_pos == other.m_pos && m_buf == other.m_buf;
                      }

                  bool operator!= ( self_type const & other) const
                  {
                         return m_pos != other.m_pos && m_buf == other.m_buf;
                      }

                  bool operator> ( self_type const & other) const
                  {
                         return m_pos > other.m_pos;
                      }

                  bool operator>= ( self_type const & other) const
                  {
                         return m_pos >= other.m_pos;
                      }

                  bool operator< ( self_type const & other ) const
                  {
                         return m_pos < other.m_pos;
                      }

                  bool operator<= ( self_type const & other ) const
                  {
                         return m_pos <= other.m_pos;
                      }



                  friend self_type operator+ (
                     typename self_type::difference_type const & lhs,
                              self_type                  const & rhs )
                  {
                         return self_type( lhs ) + rhs;
                      }

                  friend self_type operator- (
                     typename self_type::difference_type const & lhs,
                              self_type                  const & rhs )
                  {
                         return self_type( lhs ) - rhs;
                      }

               private:
                  circular_buffer_type * m_buf;
                  size_type  m_pos;
               };

        

           typedef iterator_<value_type, value_type> iterator;
           typedef iterator_<const value_type, value_type> const_iterator;

#ifdef SPM_CORE_CIRCULAR_BUFFER_COMPILER_IS_MSVC6
           typedef std::reverse_iterator<iterator,T>        reverse_iterator;
   typedef std::reverse_iterator<const_iterator, T> const_reverse_iterator;
#else
           typedef std::reverse_iterator<iterator>          reverse_iterator;
           typedef std::reverse_iterator<const_iterator>    const_reverse_iterator;
#endif

           iterator begin()
           {
                  return iterator( this, 0 );
               }

           iterator end()
           {
                  return iterator( this, size() );
               }

           const_iterator begin() const
           {
                  return const_iterator( this, 0 );
               }

           const_iterator end() const
           {
                  return const_iterator( this, size() );
               }

           reverse_iterator rbegin()
           {
                  return reverse_iterator( end() );
               }

           reverse_iterator rend()
           {
                  return reverse_iterator( begin() );
               }

           const_reverse_iterator rbegin() const
           {
                  return const_reverse_iterator( end() );
               }

           const_reverse_iterator rend() const
           {
                  return const_reverse_iterator( begin() );
               }


           bool empty() const
           {
                  return 0 == m_contents_size;
               }

           size_type capacity() const
           {
                  return m_capacity;
               }

           size_type size() const
           {
                  return m_contents_size;
               }

           size_type max_size() const
           {
                  return m_allocator.max_size();
               }

               void reserve( size_type const new_size )
           {
                  if ( new_size != capacity() )
                  {
                         circular_buffer tmp( new_size );
                         const size_type offset =
                            new_size < size() ? size() - new_size : 0;

                         tmp.assign_into( begin() + offset, end() );
                         swap( tmp );
                      }
               }

    private:
           void resize( size_type const new_size );

    public:

           reference front()
           {
                  return m_array[ m_head ];
               }

           reference back()
           {
                  return m_array[ m_tail ];
               }

           const_reference front() const
           {
                  return m_array[ m_head ];
               }

           const_reference back() const
           {
                  return m_array[ m_tail ];
               }

           const_reference operator[]( size_type const n ) const
           {
                  return at_unchecked( n );
               }

           const_reference at( size_type const n ) const
           {
                  return at_checked( n );
               }

           void clear()
           {
                  for ( size_type n = 0; n < m_contents_size; ++n )
                  {
                         m_allocator.destroy( m_array + index_to_subscript( n ) );
                      }

                  m_head = 1;
                  m_tail = m_contents_size = 0;
               }

           void push_back( value_type const & item )
           {
                  size_type next = next_tail();

                  if ( m_contents_size == m_capacity )
                  {
                         m_array[ next ] = item;
                         increment_head();
                      }
                  else
                  {
                         m_allocator.construct( m_array + next, item );
                      }
                  increment_tail();
               }

           void pop_front()
           {
                  size_type destroy_pos = m_head;
                  increment_head();
                  m_allocator.destroy( m_array + destroy_pos );
               }

           reference operator[]( size_type const n )
           {
                  return at_unchecked( n );
               }

           reference at( size_type const n )
           {
                  return at_checked( n );
               }

           pointer getimpl()
           {
                  return m_array;
               }

    private:
           reference at_unchecked( size_type const index ) const
           {
                  return m_array[ index_to_subscript( index ) ];
               }

           reference at_checked( size_type const index ) const
           {
                  if ( index >= m_contents_size )
                  {
                          throw std::out_of_range( "circular_buffer::at()" );
                      }
                  return at_unchecked( index );
               }

           size_type normalise( size_type const n ) const
           {
                  return n % m_capacity;
               }

           size_type index_to_subscript( size_type const index ) const
           {
                  return normalise( index + m_head );
               }

           void increment_tail()
           {
                  ++m_contents_size;
                  m_tail = next_tail();
               }

           size_type next_tail()
           {
                  return ( m_tail + 1 == m_capacity ) ? 0 : m_tail + 1;
               }

           void increment_head()
           {
                  ++m_head;
                  --m_contents_size;

                  if ( m_head == m_capacity )
                  {
                         m_head = 0;
                      }
               }

           template < typename I >
           void assign_into( I from, I const to )
           {
                  if ( m_contents_size > 0 )
                  {
                         clear();
                      }

                  while ( from != to )
                  {
                          push_back(*from);
                          ++from;
                      }
               }

    private:
           size_type      m_capacity;
           allocator_type m_allocator;
           pointer        m_array;
           size_type      m_head;
           size_type      m_tail;
           size_type      m_contents_size;
    };

    template < typename T, typename A >
    bool operator== ( circular_buffer<T,A> const & lhs,
                      circular_buffer<T,A> const & rhs )
{
       return lhs.size() == rhs.size()
           && equal( lhs.begin(), lhs.end(), rhs.begin() );
}

template < typename T, typename A >
bool operator!= ( circular_buffer<T,A> const & lhs,
                  circular_buffer<T,A> const & rhs )
{
   return ! ( lhs == rhs );
}

template < typename T, typename A >
bool operator< ( circular_buffer<T,A> const & lhs,
                 circular_buffer<T,A> const & rhs )
{
   return lexicographical_compare(
      lhs.begin(), lhs.end(),
      rhs.begin(), rhs.end() );
}

template < typename T, typename A >
typename circular_buffer<T,A>::iterator
begin( circular_buffer<T,A> & cb )
{
       return cb.begin();
}

template < typename T, typename A >
typename circular_buffer<T,A>::iterator
end( circular_buffer<T,A> & cb )
{
       return cb.end();
}

template < typename T, typename A >
typename circular_buffer<T,A>::const_iterator
begin( circular_buffer<const T, A> const & cb )
{
       return cb.begin();
}

template < typename T, typename A >
typename circular_buffer<const T,A>::const_iterator
end( circular_buffer<const T, A> const & cb )
{
       return cb.end();
}

